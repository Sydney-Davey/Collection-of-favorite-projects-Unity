using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    public bool isPaused = false;

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void PlayGame()
    {
        Debug.Log("Game Playing");
        isPaused = false;
        Time.timeScale = 1f;
    }
}
