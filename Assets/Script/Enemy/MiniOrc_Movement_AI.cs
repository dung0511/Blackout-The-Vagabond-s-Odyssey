using UnityEngine;
using Pathfinding;
using System;

public class MiniOrc_Movement_AI : MonoBehaviour
{
    [SerializeField] private MovementDetailsSO movementDetails;
    private Enemy enemy;


    public Transform target;

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;

    [HideInInspector] public float moveSpeed;
    private bool chasePlayer = false;
    void Start()
    {
        enemy = GetComponent<Enemy>();

        moveSpeed = movementDetails.GetMoveSpeed();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f);
        
    }
    void UpdatePath()
    {
        if(seeker.IsDone()) seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void Update()
    {
        //if (path == null) return;
        //if (currentWaypoint >= path.vectorPath.Count)
        //{
        //    reachedEndOfPath = true;
        //    return;
        //} else reachedEndOfPath = false;

        //Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized ;

        //float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        //if (distance < nextWaypointDistance)
        //{
        //    currentWaypoint++;
        //}
        if(enemy.health > 0)
        {
            MoveEnemy();
        }
        //else if (enemy.health <= 0)
        //{
        //    enemy.health = 0;
        //}
        
    }

    private void MoveEnemy()
    {
        if (!chasePlayer && Vector3.Distance(transform.position, target.position ) < enemy.enemyDetails.chaseDistance)
        {
            chasePlayer = true;
        }
        if (!chasePlayer) return;
        enemy.movementToPositionEvent.CallMovementToPositionEvent(target.position, transform.position, moveSpeed, (target.position - transform.position).normalized);
    }

    #region Validation

#if UNITY_EDITOR

    private void OnValidate()
    {
        Utility.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
    }

#endif

    #endregion Validation
}
