
using System;
using UnityEngine;

[RequireComponent(typeof(OrcWarlock))]
[DisallowMultipleComponent]

public class OrcWarlockAnimate : MonoBehaviour
{
    private OrcWarlock enemy;

    private void Awake()
    {
        Debug.Log("OrcWarlock");
        // Load components
        enemy = GetComponent<OrcWarlock>();
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

       // enemy.animator.SetBool(AnimationParameters.isIdle, false);
        enemy.animator.SetBool(AnimationParameters.isWalk, true);
    }
    private void SetIdleAnimationParameters()
    {

        enemy.animator.SetBool(AnimationParameters.isWalk, false);
       // enemy.animator.SetBool(AnimationParameters.isIdle, true);
    }
}
public static class OrcWarlockAnimationParameters
{
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isWalk = Animator.StringToHash("isMoving");
    public static int isAttack = Animator.StringToHash("isAtatck");
    public static int isDead = Animator.StringToHash("isDead");

}
