using UnityEngine;

public class AimController : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;

    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Camera.main.nearClipPlane;


        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        transform.position = worldPosition;

        //transform.position = new Vector3(transform.position.x+1, transform.position.y,transform.position.z);
    }
}
