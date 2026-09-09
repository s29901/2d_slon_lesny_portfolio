using UnityEngine;

public class ClickAnywhereSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // левая кнопка мыши
        {
            if (clickSound != null)
                audioSource.PlayOneShot(clickSound);
        }
    }
}