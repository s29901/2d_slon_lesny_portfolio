using System.Collections;
using UnityEngine;

/// <summary>
/// Shows a sign once the skeleton is complete — and, ideally, carries the
/// button back to the museum, so the player is told both that they are done
/// and what to do about it.
///
/// Attach to any object in the excavation scene and drag the sign into
/// <see cref="sign"/>. The sign appears with a fade when the last bone is
/// placed, and is already visible if the player returns to a finished scene.
/// </summary>
public class PuzzleCompleteSign : MonoBehaviour
{
    [Tooltip("The object to reveal. Put the 'back to the museum' button on it.")]
    [SerializeField] private GameObject sign;

    [Tooltip("Add a CanvasGroup to the sign for a fade; without one it simply appears.")]
    [SerializeField] private float fadeDuration = 0.6f;

    [Tooltip("Played once when the skeleton is finished.")]
    [SerializeField] private AudioClip sound;

    [Header("Attention")]
    [Tooltip("Optional: something that gently pulses while the sign is up — the button, for instance.")]
    [SerializeField] private Transform pulse;

    [SerializeField] private float pulseAmount = 0.08f;
    [SerializeField] private float pulseSpeed = 2.5f;

    private CanvasGroup group;
    private Vector3 pulseBaseScale;
    private bool shown;

    private void Awake()
    {
        if (sign != null) group = sign.GetComponent<CanvasGroup>();
        if (pulse != null) pulseBaseScale = pulse.localScale;
    }

    private void OnEnable()
    {
        BonePuzzleManager.Completed += OnCompleted;
    }

    private void OnDisable()
    {
        BonePuzzleManager.Completed -= OnCompleted;
    }

    private void Start()
    {
        if (sign == null)
        {
            Debug.LogWarning($"[PuzzleCompleteSign] {name}: no sign assigned.", this);
            return;
        }

        // coming back to a scene that was already finished: show it straight away
        if (GameProgress.PuzzleCompleted) Reveal(false);
        else sign.SetActive(false);
    }

    private void OnCompleted()
    {
        Reveal(true);
    }

    private void Reveal(bool animated)
    {
        if (sign == null || shown) return;

        shown = true;
        sign.SetActive(true);

        if (sound != null)
            AudioSource.PlayClipAtPoint(sound, Camera.main != null ? Camera.main.transform.position : Vector3.zero,
                                        VolumeManager.Instance.Get(AudioChannel.Sfx));

        if (group == null) return;

        if (animated) StartCoroutine(FadeIn());
        else group.alpha = 1f;
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        group.alpha = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        group.alpha = 1f;
    }

    private void Update()
    {
        if (!shown || pulse == null) return;

        float k = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        pulse.localScale = pulseBaseScale * k;
    }
}
