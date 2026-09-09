using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
