using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct RopeParams
{
    [Range(0.01f,1f)] public float forceRelaxation;
    public float shortestRope;
    public float forceToPerfectCenter;
    public float minForce;
    public float maxForcePassed;
    public float elasticity;
    public float dampingForce;
    public float segmentLength;
    public float forceScalingFactor;
}
public class RopeJunction : MonoBehaviour
{
    private int segmentIndex;
    private int segmentCount;
    private Transform leftPlayer, rightPlayer;
    private RopeJunction leftNeighbour, rightNeighbour;
    RopeParams ropeParams;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetParameters(int segmentIndex, int segmentCount, Transform leftPlayer, Transform rightPlayer,RopeParams ropeParams)
    {
        this.ropeParams = ropeParams;
        this.leftPlayer = leftPlayer;
        this.rightPlayer = rightPlayer;
        this.segmentIndex = segmentIndex;
        this.segmentCount = segmentCount;
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
        
        Vector3 correction = direction.normalized * (distance - ropeParams.segmentLength);
        return correction * ropeParams.elasticity;
    }

    public void PassForce(float force, bool isRight)
    {
        if (force <= ropeParams.minForce) return;

        RopeJunction neighbour = isRight ? rightNeighbour : leftNeighbour;
        if (neighbour == null) return;

        Vector3 direction = (neighbour.transform.position - transform.position).normalized;
        float distanceToNeighbour = Vector3.Distance(transform.position, neighbour.transform.position);
        
        if (distanceToNeighbour > ropeParams.segmentLength)
        {
            float excessDistance = distanceToNeighbour - ropeParams.segmentLength;
            force += excessDistance * ropeParams.forceScalingFactor;
        }

        force = ApplyForce(force, neighbour, direction);
        neighbour.PassForce(force * ropeParams.forceRelaxation, isRight);
    }

    private float ApplyForce(float force, RopeJunction neighbour, Vector3 direction)
    {
        force = Mathf.Clamp(force, ropeParams.minForce, ropeParams.maxForcePassed);
        force += Random.Range(-0.1f, 0.1f);

        neighbour.rb.AddForce(direction * (-force));

        Vector3 segmentPlacementPosition = CalculateSegmentPlacement();
        Vector3 segmentDirection = (segmentPlacementPosition - transform.position).normalized;
        neighbour.rb.AddForce(segmentDirection * (force * ropeParams.forceToPerfectCenter));

        return force;
    }

    private void ApplyDamping()
    {
        Vector3 dampingForce = -rb.linearVelocity * ropeParams.dampingForce;
        rb.AddForce(dampingForce);
    }

    private void OnDrawGizmos()
    {
        if (rb == null) return;

        float forceMagnitude = rb.linearVelocity.magnitude;
        
        Color forceColor = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(forceMagnitude / 10f));
        Gizmos.color = forceColor;
        
        Gizmos.DrawSphere(transform.position, 0.05f);
        
        Vector3 velocityDirection = rb.linearVelocity.normalized;
        Vector3 velocityEndPoint = transform.position + velocityDirection * Mathf.Clamp(forceMagnitude, 0f, 10f);
        Gizmos.DrawLine(transform.position, velocityEndPoint);

        Gizmos.color = Color.white;
        Vector3 segmentPlacement = CalculateSegmentPlacement();
        Gizmos.DrawSphere(segmentPlacement, 0.04f); 
    }

    private Vector3 CalculateSegmentPlacement()
    {
        float segmentFraction = (float)(segmentIndex + 1) / segmentCount;
        Vector3 direction = (rightPlayer.position - leftPlayer.position).normalized;
        return leftPlayer.position + direction * segmentFraction * Vector3.Distance(leftPlayer.position, rightPlayer.position);
    }

}
