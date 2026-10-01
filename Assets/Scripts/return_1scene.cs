using UnityEngine;
using UnityEngine.SceneManagement;

public class return_1scene : MonoBehaviour
{

    public GameObject Player;
    public void NextScene()
    {
        Debug.Log("NextScene() вызван");
        // the player's position is saved automatically when the scene unloads
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}