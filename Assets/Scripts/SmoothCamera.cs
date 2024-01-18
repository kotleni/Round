using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    private float speed = 2.0f;

    [SerializeField] private Transform originalTarget;
    [SerializeField] private Collider2D cameraBounds;

    private Transform target;
    private Camera mainCamera;
    private bool _isReTargered = false;

    public static SmoothCamera instance;

    public SmoothCamera()
    {
        instance = this;
    }
    
    void Start()
    {
        target = originalTarget;
        mainCamera = GetComponent<Camera>();
    }

    private void UpdateSmoothCamera()
    {
        // Prevent camera shaking
        var distance = Vector3.Distance(transform.position, target.transform.position);
        if(distance < 1f)
            return;
        
        var position = transform.position;
        position = Vector3.Lerp(position, target.transform.position, speed * Time.deltaTime);

        // Get the bounds of the camera boundary.
        Bounds bounds = cameraBounds.bounds;
        
        // Calculate camera half size
        float cameraHalfWidth = mainCamera.orthographicSize * ((float)Screen.width / Screen.height);
        float cameraHalfHeight = mainCamera.orthographicSize;

        // Adjust bounds considering camera size
        float minX = bounds.min.x + cameraHalfWidth;
        float maxX = bounds.max.x - cameraHalfWidth;
        float minY = bounds.min.y + cameraHalfHeight;
        float maxY = bounds.max.y - cameraHalfHeight;

        // Ensure the camera stays within the adjusted bounds.
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        // Force move the camera to -10 Z
        position.z = -10f;

        transform.position = position;
    }

    private void UpdateQuickCamera()
    {
        var position = target.transform.position;

        // Get the bounds of the camera boundary.
        Bounds bounds = cameraBounds.bounds;
        
        // Calculate camera half size
        float cameraHalfWidth = mainCamera.orthographicSize * ((float)Screen.width / Screen.height);
        float cameraHalfHeight = mainCamera.orthographicSize;

        // Adjust bounds considering camera size
        float minX = bounds.min.x + cameraHalfWidth;
        float maxX = bounds.max.x - cameraHalfWidth;
        float minY = bounds.min.y + cameraHalfHeight;
        float maxY = bounds.max.y - cameraHalfHeight;

        // Ensure the camera stays within the adjusted bounds.
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        
        // Force move the camera to -10 Z
        position.z = -10f;

        transform.position = position;
    }
    
    void Update()
    {
        mainCamera.orthographicSize = GameSettings.cameraSize;
        
        if(GameSettings.isDisableSmoothCamera)
            UpdateQuickCamera();
        else
            UpdateSmoothCamera();
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        _isReTargered = true;
    }

    public void ResetTarget()
    {
        target = originalTarget;
        _isReTargered = false;
    }
    
    public bool IsReTargered() {
        return _isReTargered;
    }

    void OnDrawGizmos()
    {
        if (cameraBounds != null)
        {
            // Draw gizmos to visualize the camera boundaries
            Gizmos.color = Color.blue; // You can change the color as needed
            Bounds bounds = cameraBounds.bounds;

            // Adjust bounds considering camera size
            float minX = bounds.min.x;
            float maxX = bounds.max.x;
            float minY = bounds.min.y;
            float maxY = bounds.max.y;

            // Draw the bounds using Gizmos.DrawLine
            Vector3 topLeft = new Vector3(minX, maxY, 0);
            Vector3 topRight = new Vector3(maxX, maxY, 0);
            Vector3 bottomLeft = new Vector3(minX, minY, 0);
            Vector3 bottomRight = new Vector3(maxX, minY, 0);

            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}