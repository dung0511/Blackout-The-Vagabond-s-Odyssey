using System.Collections;
using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{
    [SerializeField] private Transform projectileVisual;
    [SerializeField] private Transform projectileShadow;
    [SerializeField] private Projectile projectile;

    
    private Vector3 targetPosition;
    private Vector3 trajectoryStartPosition;

    private float shadowPositionYDivider = 6f;

    private void Start()
    {
        trajectoryStartPosition = transform.position;
    }

    private void Update()
    {
        UpdateProjectileRotation();
       
        // UpdateShadowPosition();
    }

    private void UpdateShadowPosition()
    {
        
        Vector3 trajectoryRange = targetPosition - trajectoryStartPosition;
        Vector3 newPosition = transform.position;
        newPosition.y = trajectoryStartPosition.y + projectile.GetNextYTrajectoryPosition() / shadowPositionYDivider + projectile.GetNextPositionYCorrectionAbsolute();
        projectileShadow.position = newPosition;
    }

    private void UpdateProjectileRotation()
    {
        Vector3 projectileMoveDir = projectile.GetProjectileMoveDir();
        float angle = Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg;
        projectileVisual.rotation = Quaternion.Euler(0, 0, angle);
      
        // projectileShadow.rotation = Quaternion.Euler(0, 0, angle);
    }

   
    public void SetTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }
}
