using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 3f;

    private Transform parentTransform;
    private Vector3 offset; 

    private void Awake()
    {
       
        parentTransform = transform.parent;
        offset = transform.localPosition;
        
        transform.parent = null;
    }

    private void Start()
    {
       
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (target == null) return;

       
        if (parentTransform != null)
            transform.position = parentTransform.TransformPoint(offset);

        
        Vector2 direction = target.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
}
