using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOutScript : MonoBehaviour
{
    [SerializeField] LayerMask destroyOBJwhenExit; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("NPC")) //if npc enter this will destory
        {
            Destroy(collision.gameObject);
        }
    }
}
