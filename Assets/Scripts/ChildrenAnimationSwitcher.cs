using UnityEngine;

public class ChildrenAnimationSwitcher : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (GameManager.Instance != null && GameManager.Instance.IsPuzzleCompleted)
        {
            animator.SetBool("IsPuzzleCompleted", true);
        }
    }
}