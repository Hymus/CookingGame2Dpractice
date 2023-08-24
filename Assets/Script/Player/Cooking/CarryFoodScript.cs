using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarryFoodScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer foodSpriterender; //render of food on back
    public FoodData _foodData { get; private set; } //store food data on back
    [SerializeField] Image qualityValue;
    [SerializeField] GameObject qualityBarOBj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PutDownFood();
        }

        UpdateFoodQualityBar();
    }

    void UpdateFoodQualityBar() //Update quality bar of food realtime ref value from time that store in food data
    {
        if (!_foodData) return;

        float qualityPercent = ((Time.time - _foodData._cookedTime) * 100) / _foodData._foodLifeTime;

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

    public void PickUpfood(FoodData foodData)
    {
        if (foodData != null) //if there is food in carry will put down old food before pick up new food
        {
            PutDownFood();
        }

        this._foodData = foodData; //set data and sprite to food data that recieve 
        foodSpriterender.sprite = foodData._foodSprite;

        qualityBarOBj.SetActive(true); //show quality bar of food to player

        float qualityPercent = ((Time.time - _foodData._cookedTime) * 100) / _foodData._foodLifeTime; //find the value in percent of food data
        switch (qualityPercent) //check percent of life of food and set color to match the value if good set it green if bad set yellow if alomost rotten set it red
        {
            case >= 80: qualityValue.color = Color.red; break;
            case >= 50: qualityValue.color = Color.yellow; break;
            case < 50: qualityValue.color = Color.green; break;
        }

    }

    public void PutDownFood()
    {
        if (_foodData == null) return;

        GameObject dropedFoodOBJ = Instantiate(_foodData._groundItem, transform.parent.position + transform.parent.transform.right /*new Vector3(1, 0, 0)*/, Quaternion.identity); //drop food on front of player
        dropedFoodOBJ.GetComponent<FoodGroundItem>().InitFoodData(_foodData);
        ClearFoodCarry(); //clear all data of food cuz player drop it

    }

    public void ClearFoodCarry() //clear food data and sprite
    {
        foodSpriterender.sprite = null;
        _foodData = null; 
        qualityBarOBj.SetActive(false); //disable quality bar cuz not food in back now
    }
}
