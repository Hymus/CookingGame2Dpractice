using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MenuButtonScript : MonoBehaviour
{
    public event Action OnCookingFood; //event that will invoke subscript method when click cook button

    FoodData foodData; //data of this button
    [SerializeField] private Image buttonImage; //show image of food of this button
    [SerializeField] private TextMeshProUGUI menuName; //name of food of this button
    [SerializeField] private TextMeshProUGUI menuPrice; //show price of this food
    [SerializeField] GameObject moneypaidParticlePrefab; //particle of money that will show when make food

    KitchenScript kitchenScript; //store kitchen script
    PlayerInventoryScript inventoryScript; //ref inventory to fetch inventory data 
    PlayerInventoryData inventoryData;  // ref money and increase decrease data

    private void Start()
    {
        inventoryScript = PlayerInventoryScript.instance;
        inventoryData = inventoryScript._inventoryData;
    }

    public void SetMenuButton(KitchenScript kitchen, FoodData buttonMenu) //set up this button when create this button
    {
        kitchenScript = kitchen;
        foodData = buttonMenu;

        InitMenuButton(); //when receive and stored all require data will init this Button
    }

    public void CookFoodButton() //cook button
    {
        if(inventoryData._money < foodData._foodPrice)
        {
            //instantiat text in front of kitchen window tell player that cant cook this food
            return;
        }

        GameObject paidMoneyParticle = Instantiate(moneypaidParticlePrefab, kitchenScript.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        MoneyParticle moneyParticle = paidMoneyParticle.GetComponent<MoneyParticle>(); //fetch script from prticle obj
        moneyParticle.SetAmountOfGold("-" + foodData._foodPrice.ToString("F0")); //give amount of money that paid

        inventoryData.DecreaseMoney(foodData._foodPrice);
        kitchenScript.InitialCooking(foodData); //put food data that will cook to kitchen
        OnCookingFood?.Invoke(); // invoke method that subscript in this event like close kitchen window
    }

    public void InitMenuButton() //method for init menu button set image and name for button
    {
        buttonImage.sprite = foodData._foodSprite;
        menuName.text = foodData._foodName;
        menuPrice.text = foodData._foodPrice.ToString("F0");
    }
}
