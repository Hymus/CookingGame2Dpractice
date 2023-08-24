using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScript : MonoBehaviour
{
    [SerializeField] GameObject kitchenWindow; //ref kitchen window
    [SerializeField] GameObject nameBox; //name of this kitchen
    [SerializeField] GameObject cookingBarOBJ; //ref cookingbar obj

    PlayerRestaurant player;
    [field: SerializeField] public FoodData[] _cookAbleFoods { get; private set; } //food data that can cook in this kitchen
    FoodData currentCookData; //food data that current cooking

    [SerializeField] Image cookingBarValue; //value of cooking bars
    [SerializeField] KitchenWindowScript kitchenWindowScript; //script of kitchen window

    bool isCooking; //check that is cooking or not

    float cookingCountTime; //time that start cooking
    private void Awake()
    {
        foreach(var food in _cookAbleFoods)
        {
            food.InitMaterialDictionary();
        }
    }
    private void Start()
    {
        player = PlayerRestaurant.Instance;
    }

    private void Update()
    {
        CheckCookingDistance(); //check if should close window when player stay too far away

        Cooking(); //update cooking bar
    }

    public void InitialCooking(FoodData foodData) //init cooking bar and mark that this kitchen still cooking
    {
        cookingCountTime = 0; //make it 0 count it from 0
        currentCookData = Instantiate(foodData); //stored foodata that cooking by copy it so can't make change to base food data
        isCooking = true; //mark that this kitchen still cooking
        cookingBarOBJ.SetActive(true); //show cooking bar
    }

    public void Cooking() //update cooking bar while check if it done cooking
    {
        if(isCooking)
        {
            cookingCountTime += Time.deltaTime; //count time up
            cookingBarValue.fillAmount = ((cookingCountTime * 100) / currentCookData._cookingTime) / 100;

            if (cookingCountTime >= currentCookData._cookingTime) 
            {
                Cooked(); //when done cook will call method that will intantiate food and disable cooking bar
            }
        }
    }

    public void Cooked()
    {
        currentCookData.InitCookedTime(); //ref cooked done time in data to use check quality
        GameObject foodGroundItem = Instantiate(currentCookData._groundItem, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        foodGroundItem.GetComponent<FoodGroundItem>().InitFoodData(currentCookData); //fetch script of ground item and set it too new foodData so it not bother with original foodData
        cookingBarOBJ.SetActive(false);
        isCooking = false;
    }

    private void CheckCookingDistance() //check if player is far away will close window
    {
        if (!kitchenWindow) return;

        if (kitchenWindowScript._kitchenScript == this) // if has many kitchen will check first that which kitchen is open window
        {
            if (Vector2.Distance(transform.position, player.transform.position) > 2)
            {
                kitchenWindow.SetActive(false);
            }
        }
    }

    private void OnMouseEnter()
    {
        nameBox.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) //if mouse over and left click
        {
            if (kitchenWindow.activeSelf) return; //return cuz it will close if click again when this window open

            if (isCooking) return; //if is cooking will cant open to make new food
            kitchenWindowScript.SetKitchenScriptForMenuButton(this, _cookAbleFoods);

            Debug.Log("player is " + player);
            if (Vector2.Distance(transform.position, player.transform.position) < 2) //check if player is near will open kitchen window
            {
                kitchenWindowScript.CloseAndOpenWindow();
            }
        }
    }

    private void OnMouseExit()
    {
        nameBox.SetActive(false);
    }
}
