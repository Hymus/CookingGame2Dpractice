using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemGroundScript : MonoBehaviour
{
    PlayerInventoryScript inventoryScript;
    PlayerRestaurant player;

    [field: SerializeField] public ItemData _itemData { get; private set; }

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemAmountText;

    public float _spawnTime { get; private set; }  //store the time that this item spawn so it can check when collide and stack item on ground
    private void Awake()
    {
        InitItemData(Instantiate(_itemData), 1); //copy data first in case not drop in runtime so it not bother base data
    }

    private void Start()
    {
        inventoryScript = PlayerInventoryScript.instance;
        player = PlayerRestaurant.Instance;
        _spawnTime = Time.time;
        Debug.Log("item spawn");
    }
    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0)) PickUp();
    }

    private void OnMouseEnter()
    {
        itemNameText.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        itemNameText.gameObject.SetActive(false);
    }

    public void InitItemData(ItemData itemData, int amount) //init item data when drop to ground call from dropper to set copy of original itemdata
    {
        this._itemData = Instantiate(itemData); //copy data of this item 
        this._itemData.SetItemAmount(amount); //and set amount to the amount that recieve

        UpdateGroundItemUI();
    }

    public void UpdateGroundItemUI()
    {
        itemNameText.text = this._itemData._itemName;
        itemAmountText.text = this._itemData._currentAmount.ToString();
    }
    void PickUp()
    {
        int itemFirstAmount = _itemData._currentAmount;
        for (int i = 0; i < itemFirstAmount; i++)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 2)
            {
                inventoryScript.AddItem(_itemData); //instantiate item data so it not bother with original data
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.TryGetComponent(out ItemGroundScript otherItemGroundScript)) //check collision contact obj if it has itemgroundScript too
        {
            ItemData otherItemData = otherItemGroundScript._itemData; //fetch itemdata from other ground item
            if (_itemData._itemID != otherItemData._itemID) return; //check for itemdata of other item if it same item by item ID
            if (_itemData._currentAmount >= _itemData._maxAmount) return; //check cur data of this item if it full yet
            if (otherItemData._currentAmount >= otherItemData._maxAmount) return; //check cur data of other item if it full yet

            if (otherItemData._currentAmount == _itemData._currentAmount) //if it same amount will check which one is spawn first
            {
                if (otherItemGroundScript._spawnTime < this._spawnTime) //if other item is spawn first otheritem will destroy this item so here just return
                {
                    return;
                }
                StackItem(otherItemData, collision.collider.gameObject, otherItemGroundScript); //call method to stack input otheritemData and groundscript and obj to argument 
                return; 
            }

            if (otherItemData._currentAmount > _itemData._currentAmount) return; //other item is has more amount they will check amount of this item and check for it if it out will destroy this item in other item script so just return here and leave the work to other item
            //if down here mean this item is has more item so let stack
            StackItem(otherItemData, collision.collider.gameObject, otherItemGroundScript);//call method to stack input otheritemData and groundscript and obj to argument 
        }
    }

    void StackItem(ItemData otherItemData, GameObject otherItemOBJ, ItemGroundScript otherItemGroundScript)
    {
        int amountAfterAdd = _itemData._currentAmount + otherItemData._currentAmount;
        int otherItemAmountLeft = amountAfterAdd - _itemData._maxAmount;

        if (otherItemAmountLeft <= 0)
        {
            Destroy(otherItemOBJ);
        }
        else
        {
            otherItemGroundScript._itemData.SetItemAmount(otherItemAmountLeft);
            otherItemGroundScript.UpdateGroundItemUI();
            amountAfterAdd -= otherItemAmountLeft;
        }

        _itemData.SetItemAmount(amountAfterAdd);
        UpdateGroundItemUI();
    }
}
