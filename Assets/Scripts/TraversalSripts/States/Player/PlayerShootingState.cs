using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingState : PlayerBaseState
{
    private readonly int SpellCastHash = Animator.StringToHash("SpellCast");

    private const float CrossFadeDuration = 0.1f;

    public PlayerShootingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(SpellCastHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && stateMachine.Animator.GetCurrentAnimatorStateInfo(0).IsName("SpellCast"))
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));

    }

    public override void Exit()
    {
        
    }

   

   
}
