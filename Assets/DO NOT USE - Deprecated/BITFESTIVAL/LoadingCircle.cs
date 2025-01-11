using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class LoadingCircle : MonoBehaviour
{
    public float pointAmount = 12, radius = 2, percent = 0.75f, brightness = 2;
    LineRenderer lineRenderer;
    List<Vector3> points;

    public CentralCrystal crystal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<Vector3>();
        for(int i = 0; i < pointAmount; i++)
        {
            float degree = i / pointAmount * 2 * Mathf.PI;
            float x = Mathf.Cos(degree) * radius + transform.position.x;
            float y = transform.position.y;
            float z = Mathf.Sin(degree) * radius + transform.position.z;
            points.Add(new Vector3(x,y,z));
        }
        lineRenderer.material = crystal.crystalMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        percent = (crystal.r + crystal.g + crystal.b) / 3;
        percent = Mathf.Clamp(percent, 0, 1);
        
        lineRenderer.positionCount = Mathf.RoundToInt(pointAmount * percent);

        if (percent == 1) lineRenderer.positionCount++;

        List<Vector3> currentPoints = points.GetRange(0, Mathf.RoundToInt(pointAmount * percent));

        if (percent == 1) currentPoints.Add(points[0]);
        lineRenderer.SetPositions(currentPoints.ToArray());

        lineRenderer.material.SetColor("_EmissionColor", new Color(crystal.r + 0.1f, crystal.g + 0.1f, crystal.b + 0.1f) * brightness);

    }
}
