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

    [Tooltip("Click point of the arrow cursor, in pixels from its top-left corner.")]
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    [Tooltip("The eye points with its middle, so its click point is the centre of the texture.")]
    [SerializeField] private bool centreEyeCursor = true;

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
        Texture2D texture = defaultCursor != null ? defaultCursor : Resources.Load<Texture2D>("cursor_default");
        Apply(texture, hotSpot);
    }

    public void SetEyeCursor()
    {
        Texture2D texture = eyeCursor != null ? eyeCursor : Resources.Load<Texture2D>("cursor_eye");

        Vector2 spot = centreEyeCursor && texture != null
            ? new Vector2(texture.width / 2f, texture.height / 2f)
            : hotSpot;

        Apply(texture, spot);
    }

    private void Apply(Texture2D texture, Vector2 spot)
    {
        Cursor.SetCursor(texture, spot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
