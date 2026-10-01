
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartButtonhandler : MonoBehaviour
{
    // Название или индекс сцены, которую надо загрузить
    public string sceneToLoad = "SampleScene";

    public void OnStartButtonClick()
    {
        // START begins a fresh game. Remove this line if you add a CONTINUE
        // button and want the menu to resume the saved game instead.
        GameProgress.Clear();

        SceneManager.LoadScene(sceneToLoad);
    }
}

