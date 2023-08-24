using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantCoreComponent : CoreComponent
{
    [SerializeField] GameObject eatingBar; //ref eating bar

    [SerializeField] Image eatingBarValue; //value of eatingBar
    [SerializeField] GameObject starScorePrefab; //prefab of star score this customer giving
    [field: SerializeField] public Transform _doorOut { get; private set; }

    public bool _foundTable { get; private set; } //check if found table or not
    public bool _isEated { get; private set; } //check if done eating;

    bool isEating; //check is still eating

    float eatTime; //time that eat
    float countEatingTime; //count time that eat

    public FoodTableScript _foodTable { get; private set; } //ref foodTable that this npc sitting

    private void Update()
    {
        EatingFood(); //update bar of eating
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<FoodTableScript>() != null)
        {
            _foodTable = collision.GetComponent<FoodTableScript>(); //fetch tablescript form collision that detected
            if (_foodTable._hasUser) return; //if already has user will not take seat

            _foundTable = true; //mark that have found table to sit now
        }
    }

    public void InitEatingFood(float eatingTime) //init by take eating time that calculate in enter of eat state
    {
        eatTime = eatingTime;
        countEatingTime = 0; // make it count at 0

        isEating = true; //tell that now eating
        _isEated = false; //tell that not done eating

        eatingBar.SetActive(true); //show eating bar
    }

    void EatingFood() //update eating bar
    {
        if (isEating)
        {
            countEatingTime += Time.deltaTime;
            eatingBarValue.fillAmount = ((countEatingTime * 100) / eatTime) / 100;

            if(countEatingTime > eatTime) //if done eating 
            {
                _isEated = true; //mark that done eated
                isEating = false; //so not eating anymore

                _foodTable.FinishEatedFood(); //clear food data and sprite on table
                eatingBar.SetActive(false); //disable eating bar
            }
        }
    }
    
    public void GivingStarScore(int starScore) //instantiate particle of stargiving that customer give
    {
        GameObject starScoreOBJ = Instantiate(starScorePrefab, _foodTable.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        StarScoreParticleScript starScoreScript = starScoreOBJ.GetComponent<StarScoreParticleScript>(); //fetch script from particle obj
        starScoreScript.SetStarScore(starScore); //set score that script
    }

    public void SetDoorOut(Transform doorout) //set doorout to check which side to walk too when ENter backHome state
    {
        _doorOut = doorout;
    }
}
