using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyParticle : ParticleScript
{
    [SerializeField] TextMeshProUGUI moneyText;

    protected override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        Destroy(gameObject); //money particle will destroy when done animation
    }

    public void SetAmountOfGold(string value)
    {
        moneyText.text = value; //set text of money to value of money customer paid
    }
}
