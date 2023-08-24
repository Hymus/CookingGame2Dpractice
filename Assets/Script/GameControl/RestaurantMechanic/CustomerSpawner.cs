using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] npcCustomers; //ref npc customer prefab that will spawn

    [SerializeField] Vector2 minMaxWaitToSpawnTime = new Vector2(5, 10); //time that will random wait time

    float lastSpawnTime; //store last time spawn npc
    float waitBeforeSpawnTime; //time must wait before spawn next npc

    private void Start()
    {
        lastSpawnTime = Time.time; //set last time first but notspawn yet cuz need to check this in SpawnCustomer();
        waitBeforeSpawnTime = Random.Range(minMaxWaitToSpawnTime.x, minMaxWaitToSpawnTime.y); //random time that will spawn first npc

        Spawn(); //spawn 1 when start
    }

    private void Update()
    {
        SpawnCustomer();
    }

    void SpawnCustomer()
    {
        if (Time.time > (lastSpawnTime + waitBeforeSpawnTime)) //if Time is more than wait time now will spawn
        {
            Spawn();
        }
    }

    void Spawn()
    {
        int randNPC = Random.Range(0, npcCustomers.Length); //random index of npc in array of prefab 
        GameObject newNPC = Instantiate(npcCustomers[randNPC], transform.position + new Vector3(3f, 0, 0), Quaternion.identity); //spawn random prefab 

        RestaurantNPC npc = newNPC.GetComponent<RestaurantNPC>();   //fetch npc script 
        RestaurantCoreComponent restaurantCore = npc._core.GetCoreComponent<RestaurantCoreComponent>(); //get restaurant core component
        restaurantCore.SetDoorOut(this.transform); //set this as doorOut for npc

        lastSpawnTime = Time.time; //store new lasttime that spawn
        waitBeforeSpawnTime = Random.Range(minMaxWaitToSpawnTime.x, minMaxWaitToSpawnTime.y); //random time before spawn next npc
    }

    public void SetMinMaxSpawnTime(Vector2 newMinMax) //method to set spawn randomTIme
    {
        minMaxWaitToSpawnTime = newMinMax;
    }
}
