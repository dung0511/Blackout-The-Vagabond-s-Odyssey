using System.Collections;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    
    public float flightDuration = 1f;

    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = new Vector3(1.5f, 1.5f, 1.5f);

    private void Start()
    {
       //flightDuration=GetComponent<Projectile>().EstimateFlightTime();
        transform.localScale = startScale;
        StartCoroutine(ExpandIndicator());
    }

    private IEnumerator ExpandIndicator()
    {
        float timer = 0f;
        while (timer < flightDuration)
        {
            timer += Time.deltaTime;
            float t = timer / flightDuration;
           
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
       
         Destroy(gameObject, 0.5f);
    }
}
