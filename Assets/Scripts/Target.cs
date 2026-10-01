using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Point-and-click movement: the player walks towards the last spot clicked,
/// never leaves the walkable shape, and the animator is driven by the
/// direction of travel.
/// </summary>
public class Target : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float persectiveScale;

    [Header("Filled automatically on Start")]
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    [Header("Walkable area")]
    [Tooltip("The shape the player must stay inside — drag the Boudary object here. " +
             "Its collider must have Is Trigger ticked, so it marks the area instead of " +
             "pushing the player out. Left empty, the player can walk anywhere.")]
    public Collider2D walkArea;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 followSpot;
    private string sceneName;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError($"[Target] {name} has no Rigidbody2D — movement disabled.", this);
            enabled = false;
            return;
        }

        if (anim == null) anim = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        if (walkArea == null)
            Debug.LogWarning($"[Target] {name}: no walkable area assigned, the player is unrestricted.", this);

        // each scene remembers its own spot, so coming back puts the player
        // where they left off rather than where they were in the other scene
        sceneName = gameObject.scene.name;

        if (GameProgress.HasPosition(sceneName))
            transform.position = GameProgress.GetPosition(sceneName);

        followSpot = Clamp(transform.position);

        EnsureCursorVisible();
    }

    private void OnDisable()
    {
        // fires when the scene is unloaded, whichever button caused it
        if (!string.IsNullOrEmpty(sceneName))
            GameProgress.SavePosition(sceneName, transform.position);
    }

    private void Update()
    {
        EnsureCursorVisible();

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            if (mainCamera == null) mainCamera = Camera.main;

            if (mainCamera != null)
            {
                Vector3 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                followSpot = Clamp(new Vector2(worldPoint.x, worldPoint.y));
            }
        }

        Vector2 direction = followSpot - rb.position;

        // the same distance at which FixedUpdate stops, so the walk animation
        // ends exactly when the character stops
        bool moving = direction.magnitude > speed * Time.fixedDeltaTime;

        if (anim != null)
        {
            // Only the dominant axis is reported, so "up" and "down" can never be
            // true at the same time as "sideways".
            Vector2 dir = moving ? direction.normalized : Vector2.zero;
            bool horizontal = Mathf.Abs(dir.x) >= Mathf.Abs(dir.y);

            anim.SetFloat("MoveX", horizontal ? dir.x : 0f);
            anim.SetFloat("MoveY", horizontal ? 0f : dir.y);
            anim.SetBool("IsMoving", moving);
        }

        if (spriteRenderer != null)
        {
            if (direction.x > 0.01f) spriteRenderer.flipX = false;
            else if (direction.x < -0.01f) spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        float distance = Vector2.Distance(rb.position, followSpot);

        Vector2 next = distance > step
            ? rb.position + (followSpot - rb.position).normalized * step
            : followSpot;

        rb.MovePosition(Clamp(next));
    }

    /// <summary>Keeps a point inside the walkable shape.</summary>
    private Vector2 Clamp(Vector2 point)
    {
        if (walkArea == null) return point;

        return walkArea.OverlapPoint(point) ? point : (Vector2)walkArea.ClosestPoint(point);
    }

    private static bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    private static void EnsureCursorVisible()
    {
        if (Cursor.visible && Cursor.lockState == CursorLockMode.None) return;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
