using UnityEngine;

public class ProjecttileManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;

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
        Debug.Log("sdlkghnkdl: "+ target.position);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ShootProjectile(target);
        }

    }

    public void ShootProjectile(Transform target)
    {
        Vector3 targetPosition = target.position;
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.InitializeProjectile(targetPosition, projectileMaxMoveSpeed, projectileMaxHeight);
        projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
    }

}