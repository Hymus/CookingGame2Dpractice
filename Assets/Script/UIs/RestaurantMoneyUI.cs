using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestaurantMoneyUI : MonoBehaviour
{
    PlayerInventoryScript inventoryScript; //ref money script

    [SerializeField] TextMeshProUGUI moneyShowText; //ref money amount in ui Text
    private void Start()
    {
        inventoryScript = PlayerInventoryScript.instance;

        UpdateMoneyUI();
        inventoryScript._inventoryData.OnMoneyChange += UpdateMoneyUI; //subscript this funtion to event that will call when value of mouney change will also change text too
    }

    public void UpdateMoneyUI()
    {
        moneyShowText.text = inventoryScript._inventoryData._money.ToString("F0"); //update text to current value of float in data
    }
}
