using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyAnimationControl : BaseAnimationControl
{
    Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    protected override Vector3 GetVelocity()
    {

        return rb.linearVelocity;

    }
}
