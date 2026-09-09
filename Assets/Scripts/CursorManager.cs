using UnityEngine;

/// <summary>
/// Owns the custom mouse cursor and keeps it alive across scene loads.
/// Created automatically when a scene is opened without one.
/// </summary>
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [Tooltip("Left empty, loaded from Resources/cursor_default.")]
    [SerializeField] private Texture2D defaultCursor;

    [Tooltip("Left empty, loaded from Resources/cursor_eye.")]
    [SerializeField] private Texture2D eyeCursor;

    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureInstanceExists()
    {
        if (Instance != null) return;
        new GameObject("CursorManager (auto-created)").AddComponent<CursorManager>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void Start()
    {
        SetDefaultCursor();
    }

    public void SetDefaultCursor()
    {
        Apply(defaultCursor != null ? defaultCursor : Resources.Load<Texture2D>("cursor_default"));
    }

    public void SetEyeCursor()
    {
        Apply(eyeCursor != null ? eyeCursor : Resources.Load<Texture2D>("cursor_eye"));
    }

    private void Apply(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotSpot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
