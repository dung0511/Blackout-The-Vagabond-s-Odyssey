using Assets.Script.Boss.StoneGolem;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 3f;

    private Transform parentTransform;
    private Vector3 offset;

    public StoneGolem stoneGolem;
    private Coroutine damageCoroutine;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthController playerHealthController = collision.GetComponent<PlayerHealthController>();
        if (playerHealthController != null)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                playerHealthController.takeDame(stoneGolem.damage);
                damageTimer = 0f;
            }
        }
    }

    private float damageTimer = 0f;
    private float damageInterval = 0.5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealthController playerHealthController = collision.GetComponent<PlayerHealthController>();
        if (playerHealthController != null)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                playerHealthController.takeDame(stoneGolem.damage);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealthController playerHealthController = collision.GetComponent<PlayerHealthController>();
        if (playerHealthController != null)
        {
          
            damageTimer = 0f;
        }
    }

}
