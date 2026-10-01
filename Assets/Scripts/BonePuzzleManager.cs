using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Counts the bones dug up at the excavation site and fills in the matching
/// piece of the skeleton. Bones already collected in an earlier session are
/// restored when the scene loads.
/// </summary>
public class BonePuzzleManager : MonoBehaviour
{
    /// <summary>Raised the moment the last bone is put in place.</summary>
    public static event System.Action Completed;

    [Tooltip("Shadow pieces of the skeleton, in the same order as the bone indices")]
    public GameObject[] boneSlots;

    [Tooltip("Played when a bone is put in place")]
    public AudioClip victorySound;

    private readonly Dictionary<int, GameObject> bones = new Dictionary<int, GameObject>();
    private AudioSource audioSource;
    private int collectedCount;

    /// <summary>Called by each BonePickup in its Awake.</summary>
    public void Register(int index, GameObject bone)
    {
        bones[index] = bone;
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        foreach (GameObject slot in boneSlots)
            if (slot != null) slot.SetActive(false);

        RestoreCollectedBones();
    }

    private void RestoreCollectedBones()
    {
        for (int i = 0; i < boneSlots.Length; i++)
        {
            if (!GameProgress.IsBoneCollected(i)) continue;

            if (boneSlots[i] != null) boneSlots[i].SetActive(true);

            GameObject bone;
            if (bones.TryGetValue(i, out bone) && bone != null) bone.SetActive(false);

            collectedCount++;
        }

        if (collectedCount > 0)
            Debug.Log($"[Puzzle] restored {collectedCount}/{boneSlots.Length} bones from the save");
    }

    public void CollectBone(int index, GameObject pickedObject)
    {
        if (index < 0 || index >= boneSlots.Length) return;
        if (GameProgress.IsBoneCollected(index)) return;

        if (boneSlots[index] != null) boneSlots[index].SetActive(true);
        if (pickedObject != null) pickedObject.SetActive(false);

        GameProgress.MarkBoneCollected(index);
        collectedCount++;

        if (victorySound != null)
            audioSource.PlayOneShot(victorySound, VolumeManager.Instance.Get(AudioChannel.Sfx));

        if (collectedCount == boneSlots.Length)
        {
            if (GameManager.Instance != null) GameManager.Instance.MarkPuzzleCompleted();

            if (Completed != null) Completed();
        }
    }
}
