using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderWithCursor : MonoBehaviour
{
    public string scene_2;

    public void LoadScene()
    {
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetDefaultCursor();
        // the player's position is saved automatically when the scene unloads


        SceneManager.LoadScene(scene_2);
    }
}