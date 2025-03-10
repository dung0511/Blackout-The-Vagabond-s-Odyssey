using Assets.Script.Boss.StoneGolem;
using System;
using UnityEngine;

[RequireComponent(typeof(StoneGolem))]
[DisallowMultipleComponent]

public class StoneGolemAnimate : MonoBehaviour
{
    private StoneGolem enemy;

    private void Awake()
    {
        Debug.Log("Stone Golem2");
        // Load components
        enemy = GetComponent<StoneGolem>();
    }

    private void OnEnable()
    {

        // Subscribe to movement event
        enemy.movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;

        // Subscribe to idle event
        enemy.idleEvent.OnIdle += IdleEvent_OnIdle;
    }

    private void OnDisable()
    {
        // Unsubscribe from movement event
        enemy.movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;

        // Unsubscribe from idle event
        enemy.idleEvent.OnIdle -= IdleEvent_OnIdle;
    }

    private void IdleEvent_OnIdle(IdleEvent obj)
    {
        SetIdleAnimationParameters();
    }

    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent arg1, MovementToPositionArgs arg2)
    {
        SetMovementAnimationParameters();
    }
    private void SetMovementAnimationParameters()
    {

        //enemy.animator.SetBool(AnimationParameters.isIdle, false);
        //enemy.animator.SetBool(AnimationParameters.isWalk, true);
    }
    private void SetIdleAnimationParameters()
    {

       // enemy.animator.SetBool(AnimationParameters.isWalk, false);
        //enemy.animator.SetBool(AnimationParameters.isIdle, true);
    }
}
public static class StoneGolemAnimationParameters
{
    public static int isIdle = Animator.StringToHash("isIdle");
   // public static int isWalk = Animator.StringToHash("isWalk");
    public static int isAttack1 = Animator.StringToHash("isMeleeAtatck");
    public static int isAttack2 = Animator.StringToHash("isRangedAttack");
    public static int isAttack3 = Animator.StringToHash("isLaserCast");
    public static int isImmune = Animator.StringToHash("isImmune");
   // public static int isHurt = Animator.StringToHash("isHurt");
    public static int isDead = Animator.StringToHash("isDead");

}
