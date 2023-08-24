using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public void RemoveItem(int itemIndexInList, bool removeAll)
    {
        if(_inventoryItems[itemIndexInList]._currentAmount <= 1 || removeAll)
        {
            _inventoryItems.RemoveAt(itemIndexInList);
            OnInventoryChange?.Invoke();
            return;
        }

        _inventoryItems[itemIndexInList].RemoveItemAmount(1);
        OnInventoryChange?.Invoke();
    }
}
