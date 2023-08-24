using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenWindowScript : DynamicUIwindowScript
{
   // List<MenuButtonScript> menuButtons = new List<MenuButtonScript>(); //list of menuscript may need it ?

    [SerializeField] Transform menuListWindow; //transfrom that store and align menubutton
    [SerializeField] GameObject menuButtonPrefab; //prefab of menu button

    public KitchenScript _kitchenScript { get; private set; } //store kitchen that using now 

    protected override void Awake()
    {
        base.Awake();

        _inputHandler.OnPressedInput += CloseWindow; //to make it invoke this close method when press esc 
    }
    public void SetKitchenScriptForMenuButton(KitchenScript kitchen, FoodData[] cookAbleFoodData) //add new button by food data that certain kitchen can cook and set kitchen script for menu button and set foodData for each button
    {
        _kitchenScript = kitchen; //ref kitchen first to check that this window use which kitchen
        
        foreach(Transform menuOBJ in menuListWindow.transform) //Destroy old menu on windown first
        {
            Destroy(menuOBJ.gameObject);
        }

        for(int i = 0; i < cookAbleFoodData.Length; i++) //loop to add new menu by DataGroup that store in kitchen
        {
            GameObject menuButtonOBJ = Instantiate(menuButtonPrefab, menuListWindow); //make new menubutton to be child of content menu
            CookFoodMenuButtonScript menuButtonScript = menuButtonOBJ.GetComponent<CookFoodMenuButtonScript>(); //fetch menuButton from the button obj that just create

            menuButtonScript.SetMenuButton(_kitchenScript, cookAbleFoodData[i]); //give ref of kitchen and foodData to menuScript
            menuButtonScript.OnCookingFood += ClosingKitchenWindow; // subscript Disable window to event when press button to cook food
        }
    }

    public override void CloseWindow()
    {
        if (_escInput && _window.activeSelf && _inputHandler._currentSelectWindow == _window.name) //if it active and press esc will check if this window is selected or not 
        {
            base.CloseWindow(); //if selected will close this guide window
        }
    }

    void ClosingKitchenWindow()
    {
        _window.gameObject.SetActive(false);
    }
}
