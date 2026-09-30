using UnityEngine;

/// <summary>
/// Opens and closes one info panel. Replaces the three older scripts that did
/// the same thing (InfoTabManager, OpenerInfoTab, ToggleInfoPanel).
///
/// Attach to the object that opens the panel (a plaque, a button) and drag the
/// panel into <see cref="panel"/>. Hook <see cref="Toggle"/> to the button's
/// OnClick, or leave it to the plaque's own click handler.
/// </summary>
public class InfoPanel : MonoBehaviour
{
    [Tooltip("The panel to show and hide.")]
    [SerializeField] private GameObject panel;

    [Tooltip("Panels that must close when this one opens. Leave empty if there are none.")]
    [SerializeField] private GameObject[] closeTheseFirst;

    [Tooltip("Closed on Start, so a panel left visible in the editor does not show up in play mode.")]
    [SerializeField] private bool closeOnStart = true;

    public bool IsOpen => panel != null && panel.activeSelf;

    private void Start()
    {
        if (closeOnStart && panel != null) panel.SetActive(false);
    }

    public void Toggle()
    {
        SetOpen(!IsOpen);
    }

    public void Open()
    {
        SetOpen(true);
    }

    public void Close()
    {
        SetOpen(false);
    }

    private void SetOpen(bool open)
    {
        if (panel == null)
        {
            Debug.LogWarning($"[InfoPanel] {name}: no panel assigned.", this);
            return;
        }

        if (open && closeTheseFirst != null)
        {
            foreach (GameObject other in closeTheseFirst)
                if (other != null && other != panel) other.SetActive(false);
        }

        panel.SetActive(open);
        if (open) panel.transform.SetAsLastSibling();
    }
}
