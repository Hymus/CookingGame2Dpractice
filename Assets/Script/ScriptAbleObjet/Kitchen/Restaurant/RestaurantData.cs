using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Kitchen/Data/RestaurantData")]
public class RestaurantData : ScriptableObject
{
    public List<int> _restaurantStars = new List<int>(); //store all data of stars customer review
    public int _avarageStar { get; private set; } //starsScore that average from all review

    public void CalculateAverageStar() //method to calculating average star call everytime when add new review to list
    { 
        int starsSum = _restaurantStars.Sum(); //use linq sum all item in list

        if(_restaurantStars.Count() != 0)//if not have review yet will not calculate
            _avarageStar = starsSum / _restaurantStars.Count();
    }

}
