using UnityEngine;

public class CursorHandler: MonoBehaviour
{
    private MSCameraController cameraControls;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.cameraControls = GameObject.FindObjectOfType<MSCameraController>();
    }

    public void toggleCameraAndCursorMode()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            this.cameraControls.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            this.cameraControls.enabled = true;
        }
    }
}