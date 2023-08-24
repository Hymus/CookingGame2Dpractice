using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodGroundItem : MonoBehaviour
{
    [field: SerializeField] public FoodData _foodData { get; private set; } //data of this food
    [SerializeField] TextMeshProUGUI nameBox; //name of this food
    [SerializeField] Image qualityValue;

    PlayerRestaurant player;

    private void Awake()
    {
        nameBox.text = _foodData._foodName; //ref name from food data
    }

    private void Start()
    {
        player = PlayerRestaurant.Instance;
    }
    private void Update()
    {
        UpdateFoodQualityBar();
    }

    void UpdateFoodQualityBar() //Update quality bar of food realtime ref value from time that store in food data
    {
        float qualityPercent = ((Time.time - _foodData._cookedTime) * 100) / _foodData._foodLifeTime;

        qualityValue.fillAmount =  1 - (qualityPercent / 100);  

        if(qualityPercent >= 50 && qualityPercent < 50.1f)
        {
            qualityValue.color = Color.yellow;
        }
        if (qualityPercent >= 80 && qualityPercent < 80.1f)
        {
            qualityValue.color = Color.red;
        }
    }

    public void InitFoodData(FoodData foodData)
    {
        _foodData = foodData;
        nameBox.text = _foodData._foodName;

        float qualityPercent = ((Time.time - _foodData._cookedTime) * 100) / _foodData._foodLifeTime; //find the value in percent of food data
        switch (qualityPercent) //check percent of life of food and set color to match the value if good set it green if bad set yellow if alomost rotten set it red
        {
            case >= 80: qualityValue.color = Color.red; break;
            case >= 50: qualityValue.color = Color.yellow; break;
            case < 50: qualityValue.color = Color.green; break;
        }
    }

    private void OnMouseEnter()
    {
        nameBox.gameObject.SetActive(true); //when mouse enter show name
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) //if mouse over and left click
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 2) //check if player is near will open kitchen window
            {
                player._carryFood.PickUpfood(_foodData); //give food data to the back of player before destroy this obj
                Destroy(this.gameObject);
            }
        }
    }

    private void OnMouseExit()
    {
        nameBox.gameObject.SetActive(false); //when mouse exit stop show name
    }
}
