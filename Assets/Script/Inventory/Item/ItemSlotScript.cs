using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    PlayerInventoryScript playerInventoryScript; //ref script that control logic in inventory
    PlayerRestaurant player; //ref player

    [SerializeField] Image slotImage; //image of item in slot
    [SerializeField] GameObject nameBox; //namebox of item
    [SerializeField] TextMeshProUGUI nameText; 
    [SerializeField] GameObject dropButton; //drop button obj can hide and show
    [SerializeField] TextMeshProUGUI amountText; //amount of item
    public bool _isSelect { get; private set; } //mark if it selected

    public ItemData _itemData { get; private set; } //store item data
    private void Start()
    {
        playerInventoryScript = PlayerInventoryScript.instance; //ref importance script in start
        player = PlayerRestaurant.Instance;
    }
    public void SelectSlot()
    {
        if(!_isSelect) //if it not select will select it
        {
            if(playerInventoryScript._selectedSlot != null) //if it has old select slot will deselect it first
            {
                if(playerInventoryScript._selectedSlot._isSelect) playerInventoryScript._selectedSlot.SelectSlot(); //before call to deselect check if old selected button is mark as selected == true so if call SelectSlot(); method of old button will set it back to false
            }
            playerInventoryScript.SetSelectSlot(this); //set this slot to selected slot in inventory script
            _isSelect = true; //mark that this slot is selected
            dropButton.SetActive(true); //set drop button to visible
            return;
        }
        playerInventoryScript.SetSelectSlot(null); //if this slot is selected will set slot selected in inventoryscript to null cuz no select this slot now
        _isSelect = false; //mark that this slot is not select
        dropButton.SetActive(false); //disable drop button
    }

    public void InitItemSlot(ItemData itemData) //init item to this slot from inventory script
    { 
        _itemData = itemData; //give item data to this slot

        slotImage.sprite = _itemData._itemSprite;//set sprite of slot
        nameText.text = _itemData._itemName; //set name to item slot
        amountText.text = _itemData._currentAmount.ToString(); //set amount of this item
    }

    public void DropItem() //decrease amount and instantiate ground item if last amount will destroy this slot
    {
        if(Input.GetKey(KeyCode.LeftControl)) //if hold left ctrl will drop all item in slot
        {
            DropAndCreateGroundItem(_itemData._currentAmount); //drop item and set amount to it as current amount of item in this slot has
            playerInventoryScript.RemoveItem(playerInventoryScript._inventoryItems.IndexOf(_itemData), true); //remove item by index so it remove in correct item in list and bool is tell that will remove all item in that slot or just decrease amount of item
            return;
        }
        DropAndCreateGroundItem(1); //if not hold dow left ctrl will call method to drop one time
        playerInventoryScript.RemoveItem(playerInventoryScript._inventoryItems.IndexOf(_itemData), false); //remove item by index so it remove in correct item in list and bool is tell that will remove all item in that slot or just decrease amount of item
    }
    void DropAndCreateGroundItem(int dropItemAmount) //method to drop item and set amount to drop item
    {
        GameObject dropItem = Instantiate(_itemData._groundItem, player.transform.position + new Vector3(1, 0, 0), Quaternion.identity); //instantiate obj to ground beside players
        ItemGroundScript itemGround = dropItem.GetComponent<ItemGroundScript>(); //fetch itemground script
        itemGround.InitItemData(_itemData, dropItemAmount); //set item data and amount to item ground
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SelectSlot(); //call method select this slot
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        nameBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameBox.SetActive(false);
    }
}
