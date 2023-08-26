using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTableScript : MonoBehaviour
{
    //public GameObject _user { get; private set; }
    public bool _hasUser { get; private set; } //check if has user of this table
    public bool _hasFood { get; private set; } // check if has food on this table
    public bool _eated { get; private set; } //check if customer eated

    bool isWaitForFood; //check that this table waiting for food

    float waitingTime; //time that will wait use this for bar value at max value
    float countWaitingTime; //countdown time that wait

    public FoodData _foodDataOntable { get; private set; } //food data on table
    FoodData orderedFoodData; //food data that order

    [SerializeField] GameObject orderBubbleGameOBJ; //bubble show what food is order
    [SerializeField] GameObject nameText; //nameBox of table
    [SerializeField] GameObject waitingBarOBJ; //waiting bar tell how much time customer wainting
    [SerializeField] GameObject moneypaidParticlePrefab; //particle of money that will show when customer paid money
    [SerializeField] GameObject qualityBarOBj;

    [SerializeField] SpriteRenderer foodOrderSprite; //sprite show food that order
    [SerializeField] SpriteRenderer foodOnTableSpriteRenderer; //sprite show food that put on table

    [SerializeField] Image qualityValue;
    [SerializeField] Image waitingBarValue; //value of bar that waiting

    PlayerRestaurant player; //ref player
    PlayerInventoryScript playerInventoryScript;
    PlayerInventoryData playerInventoryData;

    private void Start()
    {
        player = PlayerRestaurant.Instance;
        playerInventoryScript = PlayerInventoryScript.instance;
        playerInventoryData = playerInventoryScript._inventoryData;
    }

    private void Update()
    {
        WaitingForFood(); //update waiting bar
        UpdateFoodQualityBar(); //update qualitybar on table
    }

    void UpdateFoodQualityBar() //Update quality bar of food realtime ref value from time that store in food data
    {
        if (!_foodDataOntable) return;

        float qualityPercent = ((Time.time - _foodDataOntable._cookedTime) * 100) / _foodDataOntable._foodLifeTime;

        qualityValue.fillAmount = 1 - (qualityPercent / 100);

        if (qualityPercent >= 50 && qualityPercent < 50.1f) //bar value color will change by value of quality
        {
            qualityValue.color = Color.yellow;
        }
        if (qualityPercent >= 80 && qualityPercent < 80.1f)
        {
            qualityValue.color = Color.red;
        }
    }

    public void TakeSeat() //call when take seat in that table in enter of waiting food state
    {
        _hasUser = true;
    }
    public void LeaveSeat() //call in enter of back home state
    {
        FinishWaitForFood(); //disable bubble and waitingbar, and mark that not wait for food now just do this to make sure cuz some time food not match with food that order customer will leave seat so must disable waiting buble and waiting bar

        _hasUser = false;  //mark no user of this table another npc can sit now
        orderedFoodData = null; //clear fooddata that order
    }

    public void FinishEatedFood() //call in restaurantCore when finish eat to clear food on table
    {
        float foodPrice = _foodDataOntable._foodPrice;

        switch(_foodDataOntable._foodQuality)
        {
            case FoodQuality.Good:
                foodPrice += foodPrice * 0.8f; // get benefit 80 percent if food is good quality
                break;
            case FoodQuality.Bad:
                foodPrice -= foodPrice * 0.2f; // get less money 20 percent if it bad quality
                break;
        }
        playerInventoryData.IncreaseMoney(foodPrice);

        GameObject paidMoneyParticle = Instantiate(moneypaidParticlePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        MoneyParticle moneyParticle = paidMoneyParticle.GetComponent<MoneyParticle>(); //fetch script from prticle obj
        moneyParticle.SetAmountOfGold("+" + foodPrice.ToString("F0")); //give amount of money that got

        ClearTable(); //finish eated food is mean no food now mark to false
    }
    public void ClearTable() 
    {
        _hasFood = false; //mark bool to tell that dont have food now
        _foodDataOntable = null; //clear data cuz finish eated 
        foodOnTableSpriteRenderer.sprite = null; //clear sprite too
        qualityBarOBj.SetActive(false); //when clear table will set qualitybar to disable
    }

    public void PutAwayFood() //put food away from table 
    {
        GameObject foodGroundItem = Instantiate(_foodDataOntable._groundItem, transform.position + new Vector3(2, 0.5f, 0), Quaternion.identity); //drop food to ground beside table
        foodGroundItem.GetComponent<FoodGroundItem>().InitFoodData(_foodDataOntable);
        ClearTable(); //it put food away now clear table
    }

    public void FinishWaitForFood() //if food arrive will start eating no longer waiting
    {
        isWaitForFood = false; //mark that not wait for food now

        orderBubbleGameOBJ.SetActive(false); //disable waiting bubble and bar
        waitingBarOBJ.SetActive(false);
        qualityBarOBj.SetActive(false); //when finish wait for food will set qualitybar to disable
    }

    public void InitWaitingtTime(float waitTime) //init time that will wait
    {
        waitingBarValue.fillAmount = 1; //mnake it  at first cuz it will count down decrease

        countWaitingTime = waitTime; //make count value at max  at first to countdown
        waitingTime = waitTime;

        waitingBarOBJ.SetActive(true); //enable waiting for food bar

        isWaitForFood = true; //tell that now this table wait for food
    }

    public void WaitingForFood() //decrease waiting for food bar
    {
        if (isWaitForFood)
        {
            countWaitingTime -= Time.deltaTime; //coundown time until it out
            waitingBarValue.fillAmount = ((countWaitingTime * 100) / waitingTime) / 100f;
        }
    }

    public void OrderMenu(FoodData foodData) //init order
    {
        orderedFoodData = foodData; //store food that order

        foodOrderSprite.sprite = orderedFoodData._foodSprite; //set food sprite on bubble
        orderBubbleGameOBJ.SetActive(true); //show bubble that is waiting now
    }

    public bool CheckOrder //this check order after take food to see if it the same food that customer order or not
    {
        get
        {
            if (_foodDataOntable == null || orderedFoodData == null) return false;
            if (_foodDataOntable._foodQuality == FoodQuality.Rotten) return false; //if food is rotten will return false leave seat
            if (_foodDataOntable._foodID == orderedFoodData._foodID) return true;

            return false;
        }
    }

    private void OnMouseDown()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (_hasFood && _hasUser) return; //if has food and user so it mean they eating cant take food from table

            if (Vector2.Distance(transform.position, player.transform.position) > 2) return; //if player far away cant click

            FoodData carryFood = player._carryFood._foodData;

            if (carryFood == null) //if not carry food will check food on table if it has will take food on table to back
            {
                if (_foodDataOntable == null) return; //if no food on table too will return do no thing

                player._carryFood.PickUpfood(_foodDataOntable); //if has food on table will take that food to back
                LeaveSeat();
                ClearTable(); //call these 2 method to clear table cuz player take food now
                return;
            }

            if (_foodDataOntable != null) //if it has food on table will take food to the back of player clear back first so it not drop old food on ground
            {
                player._carryFood.ClearFoodCarry();
                player._carryFood.PickUpfood(_foodDataOntable);
            }
            else //but if there is no food will only clear the food on back dont worry about food data cuz we already store in carryFood
            {
                player._carryFood.ClearFoodCarry();
            }

            _foodDataOntable = carryFood; //give food that player carry to table
            foodOnTableSpriteRenderer.sprite = _foodDataOntable._foodSprite; //changeSprite
            orderBubbleGameOBJ.SetActive(false); //enable order food buble
            _hasFood = true; // mark that has food now

            qualityBarOBj.SetActive(true); //show quality bar of food on table

            float qualityPercent = ((Time.time - _foodDataOntable._cookedTime) * 100) / _foodDataOntable._foodLifeTime; //find the value in percent of food data
            switch(qualityPercent) //check percent of life of food and set color to match the value if good set it green if bad set yellow if alomost rotten set it red
            {
                case >= 80: qualityValue.color = Color.red; break;
                case >= 50: qualityValue.color = Color.yellow; break;
                case < 50: qualityValue.color = Color.green; break;
            }

            _foodDataOntable.UpdateFoodQuality(); //after put food to table will updata quality of that food
        }
    }

    private void OnMouseEnter()
    {
        nameText.SetActive(true);
    }
    private void OnMouseExit()
    {
        nameText.SetActive(false);  
    }
}
