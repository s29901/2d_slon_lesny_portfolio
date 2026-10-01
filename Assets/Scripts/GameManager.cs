using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Persistent game state: remembers whether the bone puzzle has been solved and
/// spawns the restored exhibit when the player comes back to the museum.
///
/// The instance normally comes from the main menu scene, but one is created
/// automatically if a scene is opened directly — so every scene is playable
/// on its own from the editor.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene names")]
    [SerializeField] private string boneCollectionSceneName = "2_scene";
    [SerializeField] private string elephantSceneName = "SampleScene";

    [Header("Exhibit")]
    [Tooltip("Left empty, the prefab is loaded from Resources/elephantPrefab.")]
    [SerializeField] private GameObject elephantPrefab;
    [SerializeField] private Vector3 elephantSpawnPosition = new Vector3(922f, 737f, -6f);

    public bool IsPuzzleCompleted { get { return GameProgress.PuzzleCompleted; } }

    private const string ExhibitName = "RestoredExhibit";

    private bool _shouldSpawnElephant;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureInstanceExists()
    {
        if (Instance != null) return;
        new GameObject("GameManager (auto-created)").AddComponent<GameManager>();
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>Called by BonePuzzleManager once every fragment has been found.</summary>
    public void MarkPuzzleCompleted()
    {
        GameProgress.PuzzleCompleted = true;
    }

    /// <summary>
    /// Kept for the bone buttons in the excavation scene; counting itself lives
    /// in BonePuzzleManager.
    /// </summary>
    public void AddBone()
    {
    }

    /// <summary>Hooked to the "back to the museum" button in the excavation scene.</summary>
    public void ReturnToMuseum()
    {
        if (IsPuzzleCompleted) _shouldSpawnElephant = true;
        SceneManager.LoadScene(elephantSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == boneCollectionSceneName) _shouldSpawnElephant = false;

        // the exhibit stands restored whenever the puzzle is done — also after
        // quitting and starting the game again, not only right after the walk back
        if (scene.name == elephantSceneName && GameProgress.PuzzleCompleted)
        {
            if (GameObject.Find(ExhibitName) == null) SpawnElephant();
            _shouldSpawnElephant = false;
        }
    }

    private void SpawnElephant()
    {
        GameObject prefab = elephantPrefab != null
            ? elephantPrefab
            : Resources.Load<GameObject>("elephantPrefab");

        if (prefab == null)
        {
            Debug.LogError("[GameManager] Exhibit prefab not found. Assign it in the inspector " +
                           "or keep it at Assets/Resources/elephantPrefab.prefab.");
            return;
        }

        GameObject exhibit = Instantiate(prefab, elephantSpawnPosition, Quaternion.identity);
        exhibit.name = ExhibitName;
    }
}
