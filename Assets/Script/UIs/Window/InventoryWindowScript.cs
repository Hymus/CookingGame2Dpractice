using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryWindowScript : DynamicUIwindowScript
{
    PlayerInventoryScript playerInventoryScript;

    [SerializeField] PlayerInventoryData inventoryData; //cuz this is Inventory window script must store inventoryData to do the thing
    [SerializeField] TextMeshProUGUI moneyText; //show amount money in inventory
    [SerializeField] TextMeshProUGUI capacityText; //text whow capacity of inventory
    protected override void Awake()
    {
        base.Awake();

        _inputHandler.OnPressedInput += CloseAndOpenWindow;  //subscript close open control function to event that will call when any input recieve
        inventoryData.OnMoneyChange += UpdateMoneyTextInventory; //update text of money when change value 
        UpdateMoneyTextInventory(); //update money when awak first

        playerInventoryScript = PlayerInventoryScript.instance;
        playerInventoryScript.OnInventoryChange += UpdateInventoryUI;
    }

    public override void CloseAndOpenWindow()
    {
        if (_iInput) //if press I will can open and close Inventory window
        {
            base.CloseAndOpenWindow();
            UpdateInventoryUI(); //and update amount of ui call after  base.CloseAndOpenWindow(); cuz will check if _window enable will update but if disable will not update 
        }
        if(_escInput && _window.activeSelf && _inputHandler._currentSelectWindow == _window.name) //if it active and press esc will check if this window is selected or not 
        {
            base.CloseAndOpenWindow(); //if it selected will close this window
        }
    }

    void UpdateMoneyTextInventory()  //function to update money show in inventory
    {
        if (!_window) return;
        moneyText.text = inventoryData._money.ToString(); //change amount float to string and give it to text of money text in inventory
    }

    public void UpdateInventoryUI()
    {
        if (!_window) return; //if window is disable will return no update

        playerInventoryScript.InitInventorySlots(); //this method call when inventory has change so must update InventorySlots too
        capacityText.text = $"({playerInventoryScript._inventoryItems.Count} / {inventoryData._maxSlots})";
    }
}
