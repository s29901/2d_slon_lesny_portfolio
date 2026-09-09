using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Имена сцен")]
    public string boneCollectionSceneName = "2_scene";
    public string elephantSceneName      = "SampleScene";

    [Header("Префаб слона")]
    public GameObject elephantPrefab;
    public bool IsPuzzleCompleted => _puzzleCompleted;
    // Внутренние флаги
    private bool _puzzleCompleted = false;
    private bool _shouldSpawnElephant = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Вызывается, когда пазл полностью собран в 2_scene
    /// </summary>
    public void MarkPuzzleCompleted()
    {
        _puzzleCompleted = true;
    }
    public void AddBone()
    {
        // ничего не делаем, или, если хочешь, просто лог
        Debug.Log("[GM] AddBone() вызван, но в этой версии логика в BonePuzzleManager");
    }

    /// <summary>
    /// Вызывать на кнопку «Назад в музей» в 2_scene
    /// </summary>
    public void ReturnToMuseum()
    {
        // Устанавливаем флаг только если пазл собран
        if (_puzzleCompleted)
            _shouldSpawnElephant = true;

        SceneManager.LoadScene(elephantSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Если вернулись в сцену сбора — сбросим флаг повторного спавна
        if (scene.name == boneCollectionSceneName)
        {
            _shouldSpawnElephant = false;
        }

        // Когда загрузили музей
        if (scene.name == elephantSceneName && _shouldSpawnElephant)
        {
            Vector3 spawnPos = new Vector3(922f, 737f, -6f);
            Instantiate(elephantPrefab, spawnPos, Quaternion.identity);
            _shouldSpawnElephant = false;
        }
    }
}