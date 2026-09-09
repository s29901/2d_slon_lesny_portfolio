using System.Collections;
using UnityEngine;

public class VictoryUIController : MonoBehaviour
{
    public CanvasGroup winPanel;
    public float fadeDuration = 2f;

    private bool hasFadedIn = false;

    
    void Start()
    {
        Debug.Log("[VictoryUI] Start. PuzzleCompleted = " + GameManager.Instance?.IsPuzzleCompleted);

        if (GameManager.Instance != null && GameManager.Instance.IsPuzzleCompleted && !hasFadedIn)
        {
            StartCoroutine(FadeInPanel());
        }
    }

    IEnumerator FadeInPanel()
    {
        hasFadedIn = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            winPanel.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        winPanel.alpha = 1f;
    }
}