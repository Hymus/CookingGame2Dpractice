using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMaterialDispenserScript : MonoBehaviour
{
    [SerializeField] GameObject dispenserWindow;
    [SerializeField] ItemData[] itemGiveable;

    [SerializeField] MaterialDispenserWindowScript dispenserWindowScript;
    PlayerRestaurant player;
    private void Start()
    {
        player = PlayerRestaurant.Instance;
    }

    private void Update()
    {
        CheckDispenserDistance();
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (dispenserWindow.activeSelf) return;

            dispenserWindowScript.InitDispenserWindow(this, itemGiveable);
            if(Vector2.Distance(player.transform.position, this.transform.position) < 2)
            {
                dispenserWindowScript.CloseAndOpenWindow();
            }
        }
    }

    private void CheckDispenserDistance() //check if player is far away will close window
    {
        if (!dispenserWindow) return;

        if (dispenserWindowScript._dispenserScript == this) // if has many dispenser will check first that which dispenser is open window
        {
            if (Vector2.Distance(transform.position, player.transform.position) > 2)
            {
                dispenserWindow.SetActive(false);
            }
        }
    }
}
