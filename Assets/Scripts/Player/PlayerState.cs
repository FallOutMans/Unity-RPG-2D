using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
   protected PlayerStateMachine stateMachine;
   protected Player player;
   
   protected Rigidbody2D rb;

   protected float xInput;
   protected float yInput;
   private string animBoolName;

   protected float StateTimer { get; set; }
   protected bool TriggerCalled { get; set; }

   public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
   {
      this.player = player;
      this.stateMachine = stateMachine;
      this.animBoolName = animBoolName;
   }

   public virtual void Enter()
   {
      Debug.Log("Enter " + animBoolName);
      player.Animator.SetBool(animBoolName, true);
      rb = player.Rigidbody;
      TriggerCalled = false;
   }

   public virtual void Update()
   {
      Debug.Log("Update " + animBoolName);
      StateTimer -= Time.deltaTime;
      
      xInput = Input.GetAxis("Horizontal");
      yInput = Input.GetAxis("Vertical");
      player.Animator.SetFloat("yVelocity", rb.velocity.y);
   }

   public virtual void Exit()
   {
      Debug.Log("Exit " + animBoolName);
      player.Animator.SetBool(animBoolName, false);
   }

   public virtual void AnimationFinishTrigger()
   {
      TriggerCalled = true;
   }
}
