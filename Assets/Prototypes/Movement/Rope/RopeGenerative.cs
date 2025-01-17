using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class RopeGenerative : MonoBehaviour
{
    
    [SerializeField] RopeJunction segment;
    [SerializeField] Rigidbody leftObject, rightObject;
    [SerializeField] int numberOfSegments;
    
    [Header("Rope Parameters")]
    [SerializeField] float elasticity;
    [SerializeField] float segmentLength;
    [SerializeField] float segmentMaxLenght;

    List<RopeJunction> ropeJunctions = new();
    LineRenderer lineRenderer;
    

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        float initialSpread = Vector3.Distance(leftObject.position, rightObject.position)/(numberOfSegments + 1);
        Vector3 placementDelta = (rightObject.position - leftObject.position).normalized * initialSpread;
        Vector3 startingPoint = leftObject.transform.position;

        RopeJunction leftJunction = leftObject.AddComponent<RopeJunction>();
        RopeJunction rightJunction = rightObject.AddComponent<RopeJunction>();

        leftJunction.SetParameters(elasticity, segmentLength, segmentMaxLenght);
        rightJunction.SetParameters(elasticity, segmentLength, segmentMaxLenght);


        ropeJunctions.Add(leftJunction);
        for (int i = 0; i < numberOfSegments; i++)
        {
            RopeJunction newJunction = Instantiate(segment, startingPoint + placementDelta * (i + 1), transform.rotation, transform);
            newJunction.name = $"Rope junction {i}";
            newJunction.SetParameters(elasticity, segmentLength, segmentMaxLenght);
            ropeJunctions.Add(newJunction);
        }
        ropeJunctions.Add(rightJunction);

        for (int i = 0; i < ropeJunctions.Count; i++)
        {
            RopeJunction leftNeighbour = null;
            RopeJunction rightNeighbour = null;

            if (i != 0) leftNeighbour = ropeJunctions[i - 1];
            if (i != ropeJunctions.Count - 1) rightNeighbour = ropeJunctions[i + 1];

            ropeJunctions[i].SetNeighbours(leftNeighbour, rightNeighbour);
        }


    }

    private void OnValidate()
    {
        foreach(RopeJunction j in ropeJunctions)
        {
            j.SetParameters(elasticity, segmentLength, segmentMaxLenght);
        }
    }
    
    private void FixedUpdate()
    {
        List<Vector3> junctionPositions = new List<Vector3>();
        foreach (RopeJunction j in ropeJunctions)
        {
            junctionPositions.Add(j.transform.position);
        }

        lineRenderer.positionCount = ropeJunctions.Count;
        lineRenderer.SetPositions(junctionPositions.ToArray());
    }
}
