using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantScript : MonoBehaviour
{
    public event Action OnUpdateRestaurantStarScore;

    public static RestaurantScript instance; //make it can access in global in this scene cuz only have 1 in scene
    [SerializeField] CustomerSpawner _customerSpawner; //ref spawner of customer

    [field: SerializeField] public RestaurantData _restaurantData { get; private set; } //store restautant data
    [SerializeField] Image starScore; //image of starScore

    private void Awake()
    {
        instance = this;
        UpdateAveragerStarScore(); // update score stars in awake
    }

    public void UpdateAveragerStarScore() //call to update star in ui and in data
    {
        _restaurantData.CalculateAverageStar();
        starScore.fillAmount = (float)((_restaurantData._avarageStar * 100) / 5) / 100;

        _customerSpawner.SetMinMaxSpawnTime(new Vector2(15 - (_restaurantData._avarageStar * 2),
            70 - (_restaurantData._avarageStar * 10))); //check if restautant has star low or high if low customer will barely spawn if hight customer will often spawn 

        OnUpdateRestaurantStarScore?.Invoke();
    }
}
