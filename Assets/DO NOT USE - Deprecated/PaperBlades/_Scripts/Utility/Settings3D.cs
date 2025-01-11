using System;
using UnityEngine;


[Serializable]
public class Settings3D
{
    public bool SpatialBlend => spatialBlend;
    [SerializeField] bool spatialBlend = false;
    public float MinDistance => minDistance;
    [SerializeField] float minDistance = 1;
    public float MaxDistance => maxDistance;
    [SerializeField] float maxDistance = 500;
}
