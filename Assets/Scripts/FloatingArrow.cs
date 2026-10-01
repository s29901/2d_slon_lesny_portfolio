using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A hint arrow that bobs up and down above the thing the player should click.
/// Works on a UI element and on a plain object in the scene.
///
/// Optionally disappears once the player has clicked what it points at, so it
/// stops nagging after it has done its job.
/// </summary>
public class FloatingArrow : MonoBehaviour
{
    [Header("Bobbing")]
    [Tooltip("How far it travels from its resting place — pixels for UI, world units otherwise.")]
    [SerializeField] private float distance = 12f;

    [SerializeField] private float speed = 2f;

    [Tooltip("Bob sideways instead of up and down.")]
    [SerializeField] private bool horizontal = false;

    [Header("Drawing order")]
    [Tooltip("Moves the arrow to the bottom of its parent's child list on start, so it " +
             "draws on top of its neighbours. Only affects siblings — if the arrow sits " +
             "inside a branch that is itself covered, move that branch down instead.")]
    [SerializeField] private bool drawOnTopOfSiblings = true;

    [Header("Disappearing")]
    [Tooltip("Optional: the button this arrow points at. Once it is clicked, the arrow fades away.")]
    [SerializeField] private Button hideWhenClicked;

    [SerializeField] private float fadeDuration = 0.3f;

    private RectTransform rect;
    private CanvasGroup group;
    private Vector2 baseAnchored;
    private Vector3 basePosition;
    private bool hiding;

    private void Awake()
    {
        rect = transform as RectTransform;

        if (rect != null) baseAnchored = rect.anchoredPosition;
        else basePosition = transform.localPosition;

        group = GetComponent<CanvasGroup>();

        if (drawOnTopOfSiblings) transform.SetAsLastSibling();
    }

    private void OnEnable()
    {
        if (hideWhenClicked != null) hideWhenClicked.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        if (hideWhenClicked != null) hideWhenClicked.onClick.RemoveListener(Hide);
    }

    private void Update()
    {
        if (hiding) return;

        float offset = Mathf.Sin(Time.time * speed) * distance;
        Vector2 shift = horizontal ? new Vector2(offset, 0f) : new Vector2(0f, offset);

        if (rect != null) rect.anchoredPosition = baseAnchored + shift;
        else transform.localPosition = basePosition + (Vector3)shift;
    }

    /// <summary>Can also be called from another button's OnClick.</summary>
    public void Hide()
    {
        if (hiding) return;

        hiding = true;

        if (group == null || fadeDuration <= 0f)
        {
            gameObject.SetActive(false);
            return;
        }

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float start = group.alpha;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(start, 0f, t / fadeDuration);
            yield return null;
        }

        group.alpha = 0f;
        gameObject.SetActive(false);
    }
}
