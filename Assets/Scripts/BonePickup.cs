using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>One bone lying at the excavation site.</summary>
public class BonePickup : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("0…5 — which fragment of the skeleton this is")]
    public int boneIndex;

    [Tooltip("The object carrying BonePuzzleManager")]
    public BonePuzzleManager puzzleManager;

    private void Awake()
    {
        // registering in Awake means the manager knows every bone before its
        // own Start runs and restores what was already dug up
        if (puzzleManager != null) puzzleManager.Register(boneIndex, gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (puzzleManager != null) puzzleManager.CollectBone(boneIndex, gameObject);
    }
}
