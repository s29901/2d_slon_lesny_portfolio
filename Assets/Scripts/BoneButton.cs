using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BoneButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        // +1 кость
        GameManager.Instance.AddBone();

        // и сразу выключаем кнопку, чтобы её нельзя было нажать ещё раз
        _button.interactable = false;
    }

    private void OnDestroy()
    {
        // чистим подписку, на всякий
        if (_button != null)
            _button.onClick.RemoveListener(OnClicked);
    }
}