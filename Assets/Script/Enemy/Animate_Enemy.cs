using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]

public class Animate_Enemy : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        // Load components
        enemy = GetComponent<Enemy>();
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
        enemy.animator.SetBool(AnimationParameters.isWalk, true);
    }
    private void SetIdleAnimationParameters()
    {

        enemy.animator.SetBool(AnimationParameters.isWalk, false);
        //enemy.animator.SetBool(AnimationParameters.isIdle, true);
    }
}
public static class AnimationParameters
{
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isWalk = Animator.StringToHash("isWalk");
    public static int isAttack1 = Animator.StringToHash("isAttack1");
    public static int isAttack2 = Animator.StringToHash("isAttack2");
    public static int isAttack3 = Animator.StringToHash("isAttack3");
    public static int isHurt = Animator.StringToHash("isHurt");
    public static int isDead = Animator.StringToHash("isDead");

}
