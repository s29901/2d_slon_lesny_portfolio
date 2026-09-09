using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderWithCursor : MonoBehaviour
{
    public string scene_2;

    public void LoadScene()
    {
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetDefaultCursor();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) PlayerMemory.Save(player.transform.position);


        SceneManager.LoadScene(scene_2);
    }
}