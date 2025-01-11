using UnityEngine;

public abstract class BaseAnimationControl : MonoBehaviour
{
    [SerializeField] float isMovingThreshold = 0.05f;
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected abstract Vector3 GetVelocity();

    protected virtual void Update()
    {
        animator.SetBool("isMoving", GetVelocity().magnitude > isMovingThreshold);
    }
}
