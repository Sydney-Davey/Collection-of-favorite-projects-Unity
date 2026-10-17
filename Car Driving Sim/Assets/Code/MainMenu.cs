using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
