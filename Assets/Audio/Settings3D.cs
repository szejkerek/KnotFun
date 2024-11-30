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

      //TODO: Extend Settings3D - Additional fields
      public float DopplerLevel => dopplerLevel;
      [SerializeField, Range(0.0f, 5.0f)] float dopplerLevel = 1f;
      public float Spread => spread;
      [SerializeField, Range(0.0f, 360.0f)] float spread = 0f;
      public AudioRolloffMode RolloffMode => rolloffMode;
      [SerializeField] AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
    }
