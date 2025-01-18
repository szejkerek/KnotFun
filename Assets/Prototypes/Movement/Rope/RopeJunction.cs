using UnityEngine;

[System.Serializable]
public struct RopeParams
{
    [Range(0.01f,1f)] public float forceRelaxation;
    public float elasticity;
    public float dampingForce;
    public float segmentLength;
}
public class RopeJunction : MonoBehaviour
{
    private RopeJunction leftNeighbour, rightNeighbour;
    RopeParams ropeParams;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetParameters(RopeParams ropeParams)
    {
        this.ropeParams = ropeParams;
    }

    public void SetNeighbours(RopeJunction leftNeighbour, RopeJunction rightNeighbour)
    {
        this.leftNeighbour = leftNeighbour;
        this.rightNeighbour = rightNeighbour;
    }

    private void FixedUpdate()
    {
        if (rightNeighbour != null)
        {
            float forceMagnitude = CalculateForce(rightNeighbour).magnitude;
            PassForce(forceMagnitude, isRight: true);
        }

        if (leftNeighbour != null)
        {
            float forceMagnitude = CalculateForce(leftNeighbour).magnitude;
            PassForce(forceMagnitude, isRight: false);
        }

        ApplyDamping();
    }

    private Vector3 CalculateForce(RopeJunction neighbour)
    {
        if (neighbour == null)
            return Vector3.zero;

        Vector3 direction = neighbour.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= ropeParams.segmentLength)
            return Vector3.zero;

        // Correct position to maintain the maximum length constraint
        Vector3 correction = direction.normalized * (distance - ropeParams.segmentLength);
        return correction * ropeParams.elasticity;
    }

    public void PassForce(float force, bool isRight)
    {
        if (force <= 0.1f) return;

        RopeJunction neighbour = isRight ? rightNeighbour : leftNeighbour;
        if (neighbour == null) return;

        Vector3 direction = (neighbour.transform.position - transform.position).normalized;
        neighbour.rb.AddForce(direction * (-force));

        // Reduce the force using relaxation and pass it further
        neighbour.PassForce(force * ropeParams.forceRelaxation, isRight);
    }

    private void ApplyDamping()
    {
        Vector3 dampingForce = -rb.linearVelocity * ropeParams.dampingForce;
        rb.AddForce(dampingForce);
    }

    private void OnDrawGizmos()
    {
        // Calculate the force magnitude applied to this junction
        float forceMagnitude = rb != null ? rb.linearVelocity.magnitude : 0f;

        // Map force magnitude to a gradient from red (low) to green (high)
        Color forceColor = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(forceMagnitude / 10f)); // Adjust max force (10f) as needed
        Gizmos.color = forceColor;

        // Draw a sphere at the junction to represent the force visually
        Gizmos.DrawSphere(transform.position, 0.05f);
    }

}
