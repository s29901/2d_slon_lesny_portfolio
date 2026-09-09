using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnToMuseumButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();

        _button.onClick.AddListener(() =>
        {
            Debug.Log("[Button] Нажата кнопка возврата в музей");

            if (CursorManager.Instance != null)
                CursorManager.Instance.SetDefaultCursor();

            GameManager.Instance.ReturnToMuseum();
        }); // ← вот тут закрывается AddListener
    } // ← и вот тут закрывается Awake
} // ← и это закрывает весь класс
