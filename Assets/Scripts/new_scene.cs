using UnityEngine;
using UnityEngine.SceneManagement;

public class new_scene : MonoBehaviour
{
    public GameObject player;

    public void NextScene()
    {
        // Сохраняем позицию игрока (если он есть)
        // the player's position is saved automatically when the scene unloads

        // Загружаем следующую сцену (если возможно)
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.LogWarning("Это последняя сцена, дальше идти некуда.");
        }
    }

    public void PreviousScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex > 0)
        {
            SceneManager.LoadScene(currentIndex - 1);
        }
        else
        {
            Debug.LogWarning("Это первая сцена, назад нельзя.");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}