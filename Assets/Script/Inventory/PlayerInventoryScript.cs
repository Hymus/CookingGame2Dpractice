using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerInventoryScript : MonoBehaviour
{
    public static PlayerInventoryScript instance;
    public event Action OnInventoryChange;

    PlayerRestaurant player;
    [field: SerializeField] public PlayerInventoryData _inventoryData { get; private set; } //data that store thing of bag player such as money, Item etc

    public List<ItemData> _inventoryItems = new List<ItemData>();

    [SerializeField] Transform parentItemSlot; //parent of all itemslot 
    [SerializeField] GameObject itemSlotPrefab;
    public ItemSlotScript _selectedSlot { get; private set; }
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = PlayerRestaurant.Instance;
    }

    public void SetSelectSlot(ItemSlotScript slotScript) => _selectedSlot = slotScript;
    public void InitInventorySlots()
    {
        foreach (Transform itemslot in parentItemSlot) //clear all slot first
        {
            Destroy(itemslot.gameObject);
        }

        foreach(ItemData itemdata in _inventoryItems)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, parentItemSlot);
            ItemSlotScript slotScript = itemSlot.GetComponent<ItemSlotScript>();
            slotScript.InitItemSlot(itemdata);
        }
    }

    public void AddItem(ItemData itemData)
    {
        foreach(ItemData item in _inventoryItems)//check if there is same item or not
        {
            if (item._itemID != itemData._itemID) continue;

            if (item._currentAmount >= item._maxAmount) continue;

            item.AddItemAmount(1);
            OnInventoryChange?.Invoke();
            return;
        }
        //if not slot left and not same item will drop here
        if(_inventoryItems.Count >= _inventoryData._maxSlots)
        {
            GameObject itemGround = Instantiate(itemData._groundItem, player.transform.position, Quaternion.identity); //drop item data and return 
            return;
        }
        //if come down here mean is mean has slot left so add to new slot
        itemData.SetItemAmount(1); //add amountTo 1

        _inventoryItems.Add(Instantiate(itemData));
        OnInventoryChange?.Invoke();
    }
    public void RemoveItem(int itemIndexInList, bool removeAll, int removeAmount = 1) //take itemIndex to remove correct item in list and bool remove all to check if it remove all item and amount to remove is amount to remove this item if no input will set default 1
    {
        if((_inventoryItems[itemIndexInList]._currentAmount <= 1 && removeAmount == 1) || removeAll || (removeAmount == _inventoryItems[itemIndexInList]._currentAmount))
        {
            Debug.Log($"{_inventoryItems[itemIndexInList]._itemName} is remove all at first if and removeAmount amount is {removeAmount} and cur amount is { _inventoryItems[itemIndexInList]._currentAmount}");
            _inventoryItems.RemoveAt(itemIndexInList);
            OnInventoryChange?.Invoke();
            return;
        }

        if (removeAmount < _inventoryItems[itemIndexInList]._currentAmount) // if amount to remove is less than _current amount of this item will remove this amount
        {
            //Debug.Log($"{_inventoryItems[itemIndexInList]._itemName} is remove all at second if");
            _inventoryItems[itemIndexInList].RemoveItemAmount(removeAmount); //remove item by amount that input but if no input will set default as 1
            OnInventoryChange?.Invoke();
            return;
        }
        //down here mean remove amount is more than current amount of this item must remove this and remove next item more
        int removeAmountLeft = removeAmount - _inventoryItems[itemIndexInList]._currentAmount;
        int itemIDtoRemoveNext = _inventoryItems[itemIndexInList]._itemID; //use to find next item to remove after remove all this item
        _inventoryItems.RemoveAt(itemIndexInList); //remove all item cuz it not have all item it require
        //Debug.Log($"item {_inventoryItems[itemIndexInList]} must remove next item amout to remove next is {removeAmountLeft}");
        OnInventoryChange?.Invoke();
        ItemData nextItemToRemove = _inventoryItems.FirstOrDefault(it => it._itemID == itemIDtoRemoveNext); //find itemdata to remove next by same item ID
        RemoveItem(_inventoryItems.IndexOf(nextItemToRemove), false, removeAmountLeft); //remove this item by recursion method
    }
}
