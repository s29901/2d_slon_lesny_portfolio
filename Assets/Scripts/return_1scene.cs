using UnityEngine;
using UnityEngine.SceneManagement;

public class return_1scene : MonoBehaviour
{

    public GameObject Player;
    public void NextScene()
    {
        Debug.Log("NextScene() вызван");
        if (Player != null)
        {
            PlayerMemory.Save(Player.transform.position);
            Debug.Log("СОХРАНЯЕМ ПОЗИЦИЮ: " + Player.transform.position);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}