using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    [Tooltip("Имя сцены главного меню (проверьте его в Build Settings)")]
    public string home_page = "MainMenu";

    // Привяжем этот метод к OnClick
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(home_page);
    }
}