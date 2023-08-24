using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
[CreateAssetMenu(menuName = "Kitchen/Data/FoodData")]
public class FoodData : ScriptableObject
{
    [field: SerializeField] public int _foodID { get; private set; } //id to ref when save data to txt file and load in game
    [field: SerializeField] public string _foodName { get; private set; }
    [field: SerializeField] public float _foodPrice { get; private set; } //price of food
    [field: SerializeField] public Sprite _foodSprite { get; private set; }

    Dictionary<ItemData, int> materialDict = new Dictionary<ItemData, int>(); //unite item data and amount in here
    [field: SerializeField] public ItemData[] _foodMaterials { get; private set; } //material to make this food
    [field: SerializeField] public int[] _materialRequireAmount { get; private set; }  //amout of each material require

    [field: SerializeField] public GameObject _groundItem { get; private set; } 

    [field: SerializeField] public float _cookingTime { get; private set; } //time that will wait until it cooked
    [field: SerializeField] public float _foodLifeTime { get; private set; }
    public float _cookedTime { get; private set; }  //time when finish cooked this food
    public FoodQuality _foodQuality { get; private set; } //quality of this food

    public void UpdateFoodQuality() //update when put on table and before customer check before eat
    {
        float foodLife = ((Time.time - _cookedTime) * 100) / _foodLifeTime; //make it as % lower % mean still not rotten 

        switch (foodLife) //check percent of life food compare to max lifeTime of food to check quality by %
        {
            case >= 100: //if more than 100% mean that food stay too long more than max life of this food
                _foodQuality = FoodQuality.Rotten;
                break;
            case >= 50: //more than 50 mean it just cold but still inside the max life
                _foodQuality = FoodQuality.Bad;
                break;
            case < 50: //less than 50 it good just put out from kitchen still hot
                _foodQuality= FoodQuality.Good;
                break;
        }
    }

    public bool CanCook(List<ItemData> itemInInventory)
    {
        Dictionary<int, int> itemIDandAmount = new Dictionary<int, int>();

        foreach(ItemData item in itemInInventory)
        {
            if(itemIDandAmount.ContainsKey(item._itemID))
            {
                itemIDandAmount[item._itemID] += item._currentAmount;
            }
            else
            {
                itemIDandAmount[item._itemID] = item._currentAmount;
            }
        }
        bool canCook = false;

        foreach (var mat in materialDict)
        {
            if (itemIDandAmount.TryGetValue(mat.Key._itemID, out int HasAmount))
            {
                Debug.Log($"amount of {mat.Key._itemName} has all in inventory is {HasAmount} mat require is {mat.Value} and bool is { HasAmount >= mat.Value}");
                canCook = HasAmount >= mat.Value; //check if amount of item has is more than or equal item mat require or not
                
                if (!canCook)
                {
                    Debug.Log($"{mat.Key._itemName} mat not match");
                    return false;
                }
                continue;
            }
            Debug.Log($"{mat.Key._itemName} not have in inventory");
            canCook = false;;
            return false;
        }
        Debug.Log("return final can cook");
        return canCook;
    }
    public void InitMaterialDictionary() //init dict of mat cuz it can't init here must call in awake in kitchen that has this food to cook
    {
        materialDict.Clear(); //clear first in case in has thing in here
        for (int i = 0; i < _foodMaterials.Length; i++) //loop to add item in dict 
        {
            materialDict.Add(_foodMaterials[i], _materialRequireAmount[i]);
        }
    }
    public void InitCookedTime() //init time when first cooked call by kitchen when cooked this food
    {
        _cookedTime = Time.time;
    }
}

public enum FoodQuality //quality of food to check how customer will do of this food
{
    Good,
    Bad,
    Rotten,
}
