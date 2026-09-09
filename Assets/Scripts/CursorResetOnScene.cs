using UnityEngine;

public class CursorResetOnScene : MonoBehaviour
{
    void Start()
    {
        if (CursorManager.Instance != null)
            CursorManager.Instance.SetDefaultCursor();
    }
}