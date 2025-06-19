using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocity(new Vector2(0f, 0f));
        if (comboCounter > 2 || Time.time - lastTimeAttacked >= comboWindow)
        {
            comboCounter = 0;
        }
        player.Animator.SetInteger("ComboCounter", comboCounter);

        StateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
        {
            rb.velocity = Vector2.zero;
        }

        if (TriggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}
