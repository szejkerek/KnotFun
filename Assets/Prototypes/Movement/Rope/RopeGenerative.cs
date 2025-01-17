using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeGenerative : MonoBehaviour
{
    [SerializeField]
    GameObject segment;
    [SerializeField]
    Rigidbody leftObject, rightObject;
    [SerializeField]
    uint numberOfSegments;
    [SerializeField]
    float elasticity, segmentLength, segmentMaxLengt;
    public List<RopeJunction> ropeJunctions = new List<RopeJunction>();
    [SerializeField]
    bool shouldUpdate = false;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        float initialSpread = Vector3.Distance(leftObject.position, rightObject.position)/(numberOfSegments + 1);
        Vector3 placementDelta = (rightObject.position - leftObject.position).normalized * initialSpread;
        Vector3 startingPoint = leftObject.transform.position;

        RopeJunction leftJunction = leftObject.AddComponent<RopeJunction>();
        RopeJunction rightJunction = rightObject.AddComponent<RopeJunction>();

        leftJunction.Initialize(elasticity, segmentLength, segmentMaxLengt);
        rightJunction.Initialize(elasticity, segmentLength, segmentMaxLengt);


        ropeJunctions.Add(leftJunction);
        for (int i = 0; i < numberOfSegments; i++)
        {
            GameObject newJunction = Instantiate(segment, startingPoint + placementDelta * (i + 1), transform.rotation, transform);
            newJunction.name = "Rope junction " + i.ToString();
            RopeJunction junction = newJunction.GetComponent<RopeJunction>();
            junction.Initialize(elasticity, segmentLength, segmentMaxLengt);
            ropeJunctions.Add(junction);
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

    private void FixedUpdate()
    {
        if (shouldUpdate) UpdateParameters();
        List<Vector3> junctionPositions = new List<Vector3>();
        foreach (RopeJunction j in ropeJunctions)
        {
            junctionPositions.Add(j.transform.position);
        }

        lineRenderer.positionCount = ropeJunctions.Count;
        lineRenderer.SetPositions(junctionPositions.ToArray());
    }

    private void UpdateParameters()
    {
        shouldUpdate = false;
        foreach(RopeJunction j in ropeJunctions)
        {
            j.Initialize(elasticity, segmentLength, segmentMaxLengt);
        }
    }
}
