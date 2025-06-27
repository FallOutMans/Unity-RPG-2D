using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;
    
    private string animBoolName;
    
    protected float stateTimer;
    protected bool triggerCalled;

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    
    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.Rigidbody;
        enemyBase.Animator.SetBool(animBoolName, true);
    }
    
    public virtual void Exit()
    {
        enemyBase.Animator.SetBool(animBoolName, false);
    }
    
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
