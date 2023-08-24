using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public int _itemID { get; private set; }
    [field: SerializeField] public string _itemName { get; private set; }
    [field: SerializeField] public ItemType _itemType { get; private set; }

    [field: SerializeField] public int _currentAmount { get; private set; }
    [field: SerializeField] public int _maxAmount { get; private set; }

    [field: SerializeField] public Sprite _itemSprite { get; private set; }
    [field: SerializeField] public GameObject _groundItem { get; private set; }

    public void AddItemAmount(int amount) => _currentAmount += amount;
    public void RemoveItemAmount(int amount) => _currentAmount -= amount;
    public void SetItemAmount(int amount) => _currentAmount = amount;
}

public enum ItemType
{
    Material
}
