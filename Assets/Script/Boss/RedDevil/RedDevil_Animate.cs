using UnityEngine;

[RequireComponent(typeof(RedDevil))]
[DisallowMultipleComponent]
public class RedDevil_Animate : MonoBehaviour
{
    private RedDevil enemy;

    private void Awake()
    {
        enemy = GetComponent<RedDevil>();
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

