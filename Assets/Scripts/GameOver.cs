
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool gameIsOver;
    
    public void gameOver()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
}
