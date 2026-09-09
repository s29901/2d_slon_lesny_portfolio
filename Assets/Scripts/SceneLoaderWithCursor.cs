using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderWithCursor : MonoBehaviour
{
    public string scene_2;

    public void LoadScene()
    {
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetDefaultCursor();
        PlayerMemory.Save(GameObject.FindWithTag("Player").transform.position);


        SceneManager.LoadScene(scene_2);
    }
}