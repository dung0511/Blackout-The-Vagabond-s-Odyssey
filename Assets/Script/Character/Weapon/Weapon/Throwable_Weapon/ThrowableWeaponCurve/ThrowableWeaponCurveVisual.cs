using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ThrowableWeaponCurveVisual : MonoBehaviour
{
    [SerializeField] private Transform projectileVisual;
    [SerializeField] private Transform projectileShadow;
    [SerializeField] private ThrowableWeaponCurveProjectile projectile;

    private Vector3 target;
    private Vector3 trajectoryStartPosition;

    private float shadowPositionDivider = 6f;

    private void Start()
    {
        trajectoryStartPosition = transform.position;
    }

    private void Update()
    {
        UpdateProjectileRotation();
        //UpdateShadowPosition();

        float trajectoryProgressMagnitude = (transform.position - trajectoryStartPosition).magnitude;
        float trajectoryMagnitude = (target - trajectoryStartPosition).magnitude;

        float trajectoryProgressNormalized = trajectoryProgressMagnitude / trajectoryMagnitude;

        if (trajectoryProgressNormalized < .7f)
        {
           // UpdateProjectileShadowRotation();
        }
    }

    private void UpdateShadowPosition()
    {
        Vector3 newPosition = transform.position;
        Vector3 trajectoryRange = target - trajectoryStartPosition;

        if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            // Projectile is curved on the X axis
            newPosition.x = trajectoryStartPosition.x + projectile.GetNextXTrajectoryPosition() / shadowPositionDivider + projectile.GetNextPositionXCorrectionAbsolute();
        }
        else
        {
            // Projectile is curved on the Y axis
            newPosition.y = trajectoryStartPosition.y + projectile.GetNextYTrajectoryPosition() / shadowPositionDivider + projectile.GetNextPositionYCorrectionAbsolute();
        }

        projectileShadow.position = newPosition;
    }

    private void UpdateProjectileRotation()
    {
        Vector3 projectileMoveDir = projectile.GetProjectileMoveDir();
        float angle = Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg;

        if (projectileMoveDir.x < 0) 
        {
            angle += 180;
        }

        projectileVisual.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void UpdateProjectileShadowRotation()
    {
        Vector3 projectileMoveDir = projectile.GetProjectileMoveDir();
        projectileShadow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg);
    }

    // Cập nhật SetTarget nhận tham số là Vector3 thay vì Transform
    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
