using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PlayerData/InventoryData")]
public class PlayerInventoryData : ScriptableObject
{
    public event Action OnMoneyChange; //invoke when change money like change text of money Ui or in inventory ui etc.
    [field: SerializeField] public int _maxSlots { get; private set; }
    [field: SerializeField] public float _money { get; private set; }

    public void IncreaseMoney(float amount) //increase money and invoke function in event
    {
        _money += amount;
        OnMoneyChange?.Invoke();
    }
    public void DecreaseMoney(float amount) //decrease money and invoke function in event
    {
        _money -= amount;
        OnMoneyChange?.Invoke();
    } 
}
