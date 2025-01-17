using System;
using UnityEngine;

public class RopeJunction : MonoBehaviour
{
    [SerializeField]
    private RopeJunction leftNeighbour, rightNeighbour;
    [SerializeField]
    private float elasticity, lenght, maxLength;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetParameters(float elasticity, float length, float maxLength)
    {
        this.elasticity = elasticity;
        this.lenght = length;
        this.maxLength = maxLength;
    }

    public void SetNeighbours(RopeJunction leftNeighbour, RopeJunction rightNeighbour)
    {
        this.leftNeighbour = leftNeighbour;
        this.rightNeighbour = rightNeighbour;
    }

    private void FixedUpdate()
    {
        PullTogether(leftNeighbour);
        PullTogether(rightNeighbour);
    }

    private void PullTogether(RopeJunction neighbour)
    {
        if (neighbour == null) return;
        float distance = Vector3.Distance(transform.position, neighbour.transform.position);

        if (distance > maxLength)
        {
            Vector3 force = (neighbour.transform.position - transform.position).normalized * (Time.deltaTime * elasticity * 10 * distance);
            rb.AddForce(force);
            neighbour.rb.AddForce(-force);
        }
        
        else if (distance > lenght)
        {
            Vector3 force = (neighbour.transform.position - transform.position).normalized * (Time.deltaTime * elasticity * distance);
            rb.AddForce(force);
            neighbour.rb.AddForce(-force);
        }
    }

}
