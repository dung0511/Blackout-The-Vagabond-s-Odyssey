using System.Collections;
using UnityEngine;

public class ProjecttileManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject targetIndicatorPrefab;

    [SerializeField] private float shootRate;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private float projectileMaxHeight;
    private float shootTimer;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;


    private void Update()
    {
        //shootTimer -= Time.deltaTime;

        //if (shootTimer <= 0)
        //{
        //    shootTimer = shootRate;

        //}


        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShootMultipleProjectiles());
        }

    }

    public void ShootProjectile(Transform target)
    {
        Vector3 targetPosition = target.position;
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.InitializeProjectile(targetPosition, projectileMaxMoveSpeed, projectileMaxHeight);
        projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
        float flightTime = projectile.EstimateFlightTime();


        GameObject indicatorGO = Instantiate(targetIndicatorPrefab, targetPosition, Quaternion.identity);
        TargetIndicator indicator = indicatorGO.GetComponent<TargetIndicator>();
        indicator.flightDuration = flightTime;
    }
    private IEnumerator ShootMultipleProjectiles()
    {
        int shots = 0;
        while (shots < 10)
        {
            ShootProjectile(target);
            shots++;
            yield return new WaitForSeconds(0.2f);
        }
    }
}