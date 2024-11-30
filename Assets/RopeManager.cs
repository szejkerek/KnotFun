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

    private void Awake()
    {
        rope = GetComponent<Rope>();
        ChangeRopeLenght(0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangeRopeLenght(0f);
            Debug.Log("0");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
             ChangeRopeLenght(1);
             Debug.Log("1");
        }
    }

    public void ChangeRopeLenght(float t)
    {
        currentRopeLength = Mathf.Lerp(ropeRenderLength.x, ropeRenderLength.y, t);
        currentPullDistance = Mathf.Lerp(ropePullDistance.x, ropePullDistance.y, t);
        
        rope.ropeLength = currentRopeLength;
        rope.RecalculateRope();
    }
}
