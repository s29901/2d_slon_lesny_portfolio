using UnityEngine;
using UnityEngine.EventSystems;

public class BonePickup : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("0…5 — номер текущей кости")]
    public int boneIndex;

    [Tooltip("Ссылка на объект с BonePuzzleManager")]
    public BonePuzzleManager puzzleManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"[BonePickup] Clicked index={boneIndex}");
        puzzleManager.CollectBone(boneIndex, gameObject); 
    }
}