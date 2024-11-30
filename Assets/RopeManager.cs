using System;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    private Rope rope;
    [SerializeField] private GameObject ropeLoop;
    [SerializeField] private Player playerA;
    [SerializeField] private Player playerB;
    [SerializeField] private Vector2 ropeRenderLength;
    [SerializeField] private Vector2 ropePullDistance;
    public float currentRopeLength;
    public float currentPullDistance;

    private void Start()
    {
        rope = GetComponent<Rope>();
        rope.Init();
        ChangeRopeLenght(playerA.PlayerAttackManager.currentCharge, playerB.PlayerAttackManager.currentCharge);
    }


    public void ChangeRopeLenght(float t1, float t2)
    {
        t1 = Mathf.Clamp01(t1);
        t2 = Mathf.Clamp01(t2);
        float t = (t1 + t2) / 2;
        currentRopeLength = Mathf.Lerp(ropeRenderLength.x, ropeRenderLength.y, t);
        currentPullDistance = Mathf.Lerp(ropePullDistance.x, ropePullDistance.y, t);
        
        rope.ropeLength = currentRopeLength;
        rope.RecalculateRope();
    }
}
