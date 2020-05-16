using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyInputHandler : MonoBehaviour
{
    private MSCameraController cameraControls;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.cameraControls = GameObject.FindObjectOfType<MSCameraController>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            this.quit();
        }
        else if (Input.GetKeyUp(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            this.toggleCursor();
        }
    }

    void toggleCursor()
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

    void quit(){
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL("http://www.google.com");
#else
         Application.Quit();
#endif
    }
}