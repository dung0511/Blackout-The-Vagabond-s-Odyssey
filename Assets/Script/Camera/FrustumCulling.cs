using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    [SerializeField, Range(1,100)] private float cullingOffset = 5.0f; // offset for culling
    [SerializeField] private GameObject[] CullingObjectsParent; 
    private Camera playerCamera;

    void Awake()
    {
        playerCamera = GetComponent<Camera>(); 
    }

    void FixedUpdate()
    {
        // Calculate camera bounds with an offset
        float cameraHalfWidth = playerCamera.orthographicSize * ((float)Screen.width / (float)Screen.height);
        float cameraRight = playerCamera.transform.position.x + cameraHalfWidth + cullingOffset;
        float cameraLeft = playerCamera.transform.position.x - cameraHalfWidth - cullingOffset;
        float cameraTop = playerCamera.transform.position.y + playerCamera.orthographicSize + cullingOffset;
        float cameraBottom = playerCamera.transform.position.y - playerCamera.orthographicSize - cullingOffset;

        foreach (GameObject parent in CullingObjectsParent)
        {
            if (parent == null) continue;
            
            foreach (Transform child in parent.transform)
            {
                Renderer objRenderer = child.GetComponent<Renderer>();
                if (objRenderer != null)
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
