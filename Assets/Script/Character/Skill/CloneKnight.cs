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
    public float timetoDes;
    private void Awake()
    {
        timetoDes = knightSkill.cloneCoolDown;
        animator = GetComponent<Animator>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        ai = GetComponent<Clone_Knight_Movement_AI>();
        Invoke(nameof(ResetClone), 7f);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    void ResetClone()
    {
        PoolManagement.Instance.ReturnBullet(gameObject, knightSkill.clonePrefab);
    }

}
