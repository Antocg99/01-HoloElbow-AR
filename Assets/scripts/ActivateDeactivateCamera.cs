using UnityEngine;

public class ActivateDeactivateCamera : MonoBehaviour
{
    public Camera cameraToActivateDeactivate;

    private bool canToggle = true;

    void Start()
    {
        // Initially deactivate the camera
        cameraToActivateDeactivate.enabled = false;
    }

    void Update()
    {
        // Check if the "X" key is pressed and the toggle is allowed
        if (Input.GetKeyDown(KeyCode.X) && canToggle)
        {
            // Disable toggling for a short period to prevent rapid toggles
            canToggle = false;

            // Toggle the current state of the camera after a one-second delay
            Invoke("ToggleCameraState", 1f);
        }
    }

    void ToggleCameraState()
    {
        // Toggle the current state of the camera (activate if deactivated, deactivate if activated)
        cameraToActivateDeactivate.enabled = !cameraToActivateDeactivate.enabled;

        // Enable toggling again
        canToggle = true;
    }
}
