using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyInputHandler : MonoBehaviour
{
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