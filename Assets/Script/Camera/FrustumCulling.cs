using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    [SerializeField, Range(1,100)] private float cullingOffset = 5.0f; // offset for culling
    [SerializeField] private GameObject[] CullingObjectsParent; 
    private Camera playerCamera;
    private float cameraHalfWidth;

    void OnEnable()
    {
        playerCamera = GetComponent<Camera>();
        cameraHalfWidth = playerCamera.orthographicSize * ((float)Screen.width / (float)Screen.height);
    }

    void FixedUpdate()
    {
        var cameraPos = playerCamera.transform.position;
        // Calculate camera bounds with offset
        float cameraRight = cameraPos.x + cameraHalfWidth + cullingOffset;
        float cameraLeft = cameraPos.x - cameraHalfWidth - cullingOffset;
        float cameraTop = cameraPos.y + playerCamera.orthographicSize + cullingOffset;
        float cameraBottom = cameraPos.y - playerCamera.orthographicSize - cullingOffset;

        foreach (GameObject parent in CullingObjectsParent)
        {
            if (parent == null) continue;
            
            foreach (Transform child in parent.transform)
            {
                if (child.TryGetComponent<Renderer>(out var objRenderer))
                {
                    Bounds bounds = objRenderer.bounds; // get sprite bounding box
                    float objLeft = bounds.min.x;
                    float objRight = bounds.max.x;
                    float objTop = bounds.max.y;
                    float objBottom = bounds.min.y;

                    // check any part of sprite in camera view
                    bool isInView = (objRight >= cameraLeft && objLeft <= cameraRight && 
                                     objTop >= cameraBottom && objBottom <= cameraTop); 

                    child.gameObject.SetActive(isInView);
                }
            }
        }
    }
}
