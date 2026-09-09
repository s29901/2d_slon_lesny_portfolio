using UnityEngine;

public class VictorySoundPlayer : MonoBehaviour
{
    public AudioClip victoryClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (GameManager.Instance != null && GameManager.Instance.IsPuzzleCompleted)
        {
            audioSource.PlayOneShot(victoryClip);
        }
    }
}