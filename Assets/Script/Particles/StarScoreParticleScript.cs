using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarScoreParticleScript : ParticleScript
{
    [SerializeField] Image starImage; //ref image of star giving

    public void SetStarScore(int starScore) //set star by recieve star that customer give in parameter 
    {
        starImage.fillAmount = (float)((starScore * 100) / 5) / 100; //and calculate to get 0.0f - 1.0f value set in fill amount of image
    }

    protected override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        Destroy(this.gameObject);
    }
}
