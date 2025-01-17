using System;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class RopeJunction : MonoBehaviour
{
    private RopeJunction leftNeighbour, rightNeighbour;
    private float elasticity, lenght, forceRelaxation;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetParameters(float elasticity, float length, float forceRelaxation)
    {
        this.elasticity = elasticity;
        this.lenght = length;
        this.forceRelaxation = forceRelaxation;
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
            PassForce(CalculateForce(rightNeighbour).magnitude, isRight: true);
        }
        if (leftNeighbour != null)
        {
            PassForce(CalculateForce(leftNeighbour).magnitude, isRight: false);
        }
    }

    private Vector3 CalculateForce(RopeJunction neighbour)
    {
        if (neighbour == null) 
            return Vector3.zero;

        Vector3 direction = neighbour.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= lenght)
            return Vector3.zero;

        // Correct position to enforce the maximum length constraint
        Vector3 correction = direction.normalized * (distance - lenght);
        Vector3 force = correction * elasticity;
        

        return force;
    }

    public void PassForce(float force, bool isRight)
    {
        if (isRight)
        {
            if (rightNeighbour == null || force <= 0.1f)
                return;
            
            Vector3 direction = (rightNeighbour.transform.position - transform.position).normalized;
            rightNeighbour.rb.AddForce(direction * (-force));
            rightNeighbour.PassForce(force*forceRelaxation, isRight:true);
        }
        else
        {
            if (leftNeighbour == null || force <= 0.1f)
                return;
            
            Vector3 direction = (leftNeighbour.transform.position - transform.position).normalized;
            leftNeighbour.rb.AddForce(direction * (-force));
            leftNeighbour.PassForce(force*forceRelaxation, isRight:false);
        }
    }
    
    
}
