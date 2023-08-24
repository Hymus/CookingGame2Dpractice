using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialDispenserButton : MonoBehaviour
{
    ItemData itemData;
    FoodMaterialDispenserScript materialDispenserScript;

    [SerializeField] Image buttonImage;
    public void SpawnItem()
    {
        GameObject groundItem = Instantiate(itemData._groundItem, materialDispenserScript.transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        ItemGroundScript groundScript = groundItem.GetComponent<ItemGroundScript>();
        groundScript.InitItemData(itemData, 1);
    }

    public void InitDispenserSlot(FoodMaterialDispenserScript materialDispenserScript, ItemData itemdata)
    {
        this.itemData = itemdata;
        this.materialDispenserScript = materialDispenserScript;

        buttonImage.sprite = itemData._itemSprite;
    }
}
