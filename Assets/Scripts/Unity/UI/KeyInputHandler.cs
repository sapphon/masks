using UnityEditor;
using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            this.quit();
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