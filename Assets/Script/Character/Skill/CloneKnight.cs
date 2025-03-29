using System.Collections;
using UnityEngine;
using static Unity.Cinemachine.CinemachineTriggerAction.ActionSettings;

public class CloneKnight : MonoBehaviour
{
    public EnemyDetailSO CloneKnightDetails;
    public KnightSkill knightSkill;

    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public Animator animator;

    [HideInInspector] public Clone_Knight_Movement_AI ai;
    private float timetoDes;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        ai = GetComponent<Clone_Knight_Movement_AI>();
    }

    private void OnEnable()
    {
       
        timetoDes = knightSkill.cloneCoolDown;
        Invoke(nameof(ResetClone), timetoDes);
    }

    private void ResetClone()
    {
        PoolManagement.Instance.ReturnBullet(gameObject, knightSkill.clonePrefab);
    }

    private void OnDisable()
    {
       
        CancelInvoke(nameof(ResetClone));
    }
}
