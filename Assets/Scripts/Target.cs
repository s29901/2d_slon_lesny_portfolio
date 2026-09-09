using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Point-and-click movement: the player walks towards the last spot clicked
/// and the animator is driven by the direction of travel.
/// </summary>
public class Target : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float persectiveScale;

    [Header("Filled automatically on Start")]
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    [Header("Bounds (declared in the scenes, not yet applied)")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 followSpot;

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

        if (PlayerMemory.HasSavedPosition) transform.position = PlayerMemory.LastPosition;
        followSpot = transform.position;

        EnsureCursorVisible();
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
                followSpot = new Vector2(worldPoint.x, worldPoint.y);
            }
        }

        Vector2 direction = followSpot - rb.position;

        if (anim != null)
        {
            anim.SetFloat("MoveX", direction.x);
            anim.SetFloat("MoveY", direction.y);
            anim.SetBool("IsMoving", direction.magnitude > 0.1f);
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

        rb.MovePosition(distance > step
            ? rb.position + (followSpot - rb.position).normalized * step
            : followSpot);
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
