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

        // wipe the save first, so the fresh scene loads with nothing restored
        GameProgress.Clear();

        SceneManager.LoadScene(initialSceneBuildIndex);

    }

}
