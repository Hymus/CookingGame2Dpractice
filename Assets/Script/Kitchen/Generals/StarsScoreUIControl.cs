using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StarsScoreUIControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject showStarRecord;
    [SerializeField] TextMeshProUGUI[] starScoreTexts; //store text that reveal value of each star
    [SerializeField] RestaurantScript restaurantScript;

    RestaurantData restaurantData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        showStarRecord.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showStarRecord.SetActive(false);
    }

    private void Awake()
    {
        restaurantData = restaurantScript._restaurantData;
        restaurantScript.OnUpdateRestaurantStarScore += UpdateStarScoreUI;
    }

    void UpdateStarScoreUI()
    {
        List<int> restaurantStar = restaurantData._restaurantStars;

        for (int i = 0; i < starScoreTexts.Length; i++)
        {
            int starCount = restaurantStar.Count(num => num == i+ 1);
            starScoreTexts[i].text = starCount.ToString();
        }
    }
}
