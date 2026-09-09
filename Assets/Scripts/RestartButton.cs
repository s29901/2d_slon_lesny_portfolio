using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public int initialSceneBuildIndex = 0;

    public void RestartToFirstScene()
    {
        // «Убиваем» единственный экземпляр
        if (GameManager.Instance != null)
            Destroy(GameManager.Instance.gameObject);

        // И грузим сцену, где в Awake создастся новый GameManager
        SceneManager.LoadScene(initialSceneBuildIndex);
        PlayerMemory.HasSavedPosition = false;
    }

}
