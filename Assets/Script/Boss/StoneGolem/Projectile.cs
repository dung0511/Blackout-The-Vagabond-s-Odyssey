using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileVisual projectileVisual;

    
    private Vector3 targetPosition;
    private float moveSpeed;
    private float maxMoveSpeed;

    private float distanceToTargetToDestroyProjectile = 1f;

    private AnimationCurve trajectoryAnimationCurve;
    private AnimationCurve axisCorrectionAnimationCurve;
    private AnimationCurve projectileSpeedAnimationCurve;

    private Vector3 trajectoryStartPoint;
    private Vector3 projectileMoveDir;
    private Vector3 trajectoryRange;

    private float nextYTrajectoryPosition;
    private float nextXTrajectoryPosition;
    private float nextPositionYCorrectionAbsolute;
    private float nextPositionXCorrectionAbsolute;

    private float trajectoryMaxRelativeHeight;

    private void Start()
    {
        trajectoryStartPoint = transform.position;
    }

    private void Update()
    {
        UpdateProjectilePosition();


        if (Vector3.Distance(transform.position, targetPosition) < distanceToTargetToDestroyProjectile)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateProjectilePosition()
    {
        trajectoryRange = targetPosition - trajectoryStartPoint;

        if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            if (trajectoryRange.y < 0)
            {
                moveSpeed = -moveSpeed;
            }
            
            UpdatePositionWithXCurve();
        }
        else
        {
            if (trajectoryRange.x < 0)
            {
                moveSpeed = -moveSpeed;
            }
            
            UpdatePositionWithYCurve();
        }
    }

    private void UpdatePositionWithXCurve()
    {
        float nextPositionY = transform.position.y + moveSpeed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;

        float nextPositionXNormalized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
        nextXTrajectoryPosition = nextPositionXNormalized * trajectoryMaxRelativeHeight;

        float nextPositionXCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);
        nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalized * trajectoryRange.x;

        if (trajectoryRange.x > 0 && trajectoryRange.y > 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }
        if (trajectoryRange.x < 0 && trajectoryRange.y < 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        float nextPositionX = trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbsolute;

        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateNextProjectileSpeed(nextPositionYNormalized);
        projectileMoveDir = newPosition - transform.position;

        transform.position = newPosition;
    }

    private void UpdatePositionWithYCurve()
    {
        float nextPositionX = transform.position.x + moveSpeed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;
        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        nextYTrajectoryPosition = nextPositionYNormalized * trajectoryMaxRelativeHeight;
        float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * trajectoryRange.y;
        float nextPositionY = trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbsolute;
        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);
        CalculateNextProjectileSpeed(nextPositionXNormalized);
        projectileMoveDir = newPosition - transform.position;
        transform.position = newPosition;
    }

    private void CalculateNextProjectileSpeed(float normalizedValue)
    {
        float nextMoveSpeedNormalized = projectileSpeedAnimationCurve.Evaluate(normalizedValue);
        moveSpeed = nextMoveSpeedNormalized * maxMoveSpeed;
    }

    
    public void InitializeProjectile(Vector3 targetPos, float maxMoveSpeed, float trajectoryMaxHeight)
    {
        targetPosition = targetPos;
        this.maxMoveSpeed = maxMoveSpeed;
        float xDistanceToTarget = targetPos.x - transform.position.x;
        this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxHeight;

        
        projectileVisual.SetTargetPosition(targetPos);
    }

    public void InitializeAnimationCurves(AnimationCurve trajectoryAnimationCurve, AnimationCurve axisCorrectionAnimationCurve, AnimationCurve projectileSpeedAnimationCurve)
    {
        this.trajectoryAnimationCurve = trajectoryAnimationCurve;
        this.axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;
        this.projectileSpeedAnimationCurve = projectileSpeedAnimationCurve;
    }

    public Vector3 GetProjectileMoveDir()
    {
        return projectileMoveDir;
    }

    public float GetNextYTrajectoryPosition()
    {
        return nextYTrajectoryPosition;
    }

    public float GetNextPositionYCorrectionAbsolute()
    {
        return nextPositionYCorrectionAbsolute;
    }
}
