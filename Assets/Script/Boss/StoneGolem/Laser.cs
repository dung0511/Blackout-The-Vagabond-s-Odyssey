using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    public float rotationSpeed = 3f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (target == null) return;

       
        Vector2 direction = target.position - transform.position;

       
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

       
        float currentAngle = transform.rotation.eulerAngles.z;

        
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

       
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
}
