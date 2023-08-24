using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool _isAbilityDone; //mark to tell that ability done or not some ability will transation to next state if ability done or some abiliy can't transtation to another state if ability not done

    public PlayerAbilityState(PlayerRestaurant player, PlayerFiniteStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTriger() //some ability done when finish animation will check from this event in animation
    {
        base.AnimationFinishTriger(); 

        _isAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();

        _isAbilityDone = false; //first when enter will set it to false cuz it just start sbility
    }
}
