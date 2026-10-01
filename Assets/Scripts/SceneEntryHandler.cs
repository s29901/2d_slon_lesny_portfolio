using UnityEngine;

/// <summary>
/// Scene setup on entry. The player's position is restored by Target itself,
/// from the per-scene save, so this only hides the panel that must start closed.
/// </summary>
public class SceneEntryHandler : MonoBehaviour
{
    [Tooltip("Kept for compatibility with the scene; no longer used.")]
    public GameObject Player;

    [Tooltip("Panel that must be closed when the scene starts.")]
    public GameObject duze_info;

    private void Start()
    {
        if (duze_info != null) duze_info.SetActive(false);
    }
}
