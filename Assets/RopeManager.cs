using System;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    private Rope rope;
    [SerializeField] private GameObject ropeLoop;
    [SerializeField] private Vector2 ropeRenderLength;
    [SerializeField] private Vector2 ropePullDistance;
    public float currentRopeLength;
    public float currentPullDistance;

    [SerializeField] private float interpolationDuration = 0.2f;

    private void Start()
    {
        rope = GetComponent<Rope>();
        rope.Init();
        ChangeRopeLenght(0f);
    }


    public void ChangeRopeLenght(float t)
    {
        currentRopeLength = Mathf.Lerp(ropeRenderLength.x, ropeRenderLength.y, t);
        currentPullDistance = Mathf.Lerp(ropePullDistance.x, ropePullDistance.y, t);
        
        rope.ropeLength = currentRopeLength;
        rope.RecalculateRope();
    }
}
