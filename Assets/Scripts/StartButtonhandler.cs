
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartButtonhandler : MonoBehaviour
{
    // Название или индекс сцены, которую надо загрузить
    public string sceneToLoad = "SampleScene";

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}

