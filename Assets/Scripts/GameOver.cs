
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool gameIsOver;
    
    public void gameOver()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
