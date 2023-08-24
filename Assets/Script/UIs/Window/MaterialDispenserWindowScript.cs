using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDispenserWindowScript : DynamicUIwindowScript
{
    [SerializeField] Transform buttonParent; //transfrom that store and align give item button
    [SerializeField] GameObject giveItemButtonPrefab; //prefab of give material button

    public FoodMaterialDispenserScript _dispenserScript { get; private set; } //store dispenser that using now 

    protected override void Awake()
    {
        base.Awake();

        _inputHandler.OnPressedInput += CloseWindow; //to make it invoke this close method when press esc 
    }

    public void InitDispenserWindow(FoodMaterialDispenserScript dispenserScript, ItemData[] itemsGiveAble)
    {
        _dispenserScript = dispenserScript;

        foreach(Transform giveitemButton in buttonParent)
        {
            Destroy(giveitemButton.gameObject);
        }

        foreach(ItemData item in itemsGiveAble)
        {
            GameObject givingButton = Instantiate(giveItemButtonPrefab, buttonParent);
            MaterialDispenserButton givingButtonScript = givingButton.GetComponent<MaterialDispenserButton>();
            givingButtonScript.InitDispenserSlot(dispenserScript, item);
        }
    }

    public override void CloseWindow()
    {
        if (_escInput && _window.activeSelf && _inputHandler._currentSelectWindow == _window.name) //if it active and press esc will check if this window is selected or not 
        {
            base.CloseWindow(); //if selected will close this guide window
        }
    }
}
