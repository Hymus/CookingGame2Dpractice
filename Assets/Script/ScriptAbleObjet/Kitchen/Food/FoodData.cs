using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Kitchen/Data/FoodData")]
public class FoodData : ScriptableObject
{
    [field: SerializeField] public int _foodID { get; private set; } //id to ref when save data to txt file and load in game
    [field: SerializeField] public string _foodName { get; private set; }
    [field: SerializeField] public float _foodPrice { get; private set; } //price of food
    [field: SerializeField] public Sprite _foodSprite { get; private set; }

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
