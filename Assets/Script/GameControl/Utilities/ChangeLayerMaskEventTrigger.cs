using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerMaskEventTrigger : MonoBehaviour //class to change layermask of stair that can go up and down it must change to default when it force npc to go down so it not detect wall and jump up no go dow
{
    [SerializeField] int oldLayer; //layer index that will change
    [SerializeField] int newLayer;

    [SerializeField] GameObject[] objToChangeTag; //obj stair that will change layer

    private void ChangeLayerToOldTrigger() //anim trigger change layer to old layer
    {
        foreach (GameObject obj in objToChangeTag)
        {
            obj.layer = oldLayer;
        }
    }

    private void ChangeLayerToNewTrigger() //trigger in anim to change layer to new layer
    {
        foreach (GameObject obj in objToChangeTag)
        {
            obj.layer = newLayer;
        }
    }
}
