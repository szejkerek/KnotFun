using System;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    private Rope rope;
    RopeLoop ropeLoop;
    [SerializeField] private Player playerA;
    [SerializeField] private Player playerB;
    [SerializeField] private Vector2 ropeRenderLength;
    [SerializeField] private Vector2 ropePullDistance;
    public float currentRopeLength;
    public float currentPullDistance;

    public Material ropeMaterial;
    public GameObject locker;

    public void SetRopeMaterial()
    {
        Material material = Instantiate(ropeMaterial);

        Material materialA = playerA.GetMainMaterial();
        Material materialB = playerB.GetMainMaterial();        

        material.SetColor("_EmissionColor", materialA.GetColor("_EmissionColor") + materialB.GetColor("_EmissionColor"));

        RopeMesh ropeMesh = GetComponent<RopeMesh>();
        
        ropeMesh.material = material;

        locker.GetComponent<MeshRenderer>().material = material;

        ropeMesh.Refresh();
    }
    
    private void Start()
    {
        rope = GetComponent<Rope>();
        ropeLoop = GetComponentInChildren<RopeLoop>();
        rope.Init();
        playerA.PlayerAttackManager.OnChargeChanged += TryUpdateRope;
        playerB.PlayerAttackManager.OnChargeChanged += TryUpdateRope;
        SetRopeMaterial();
        TryUpdateRope();
        ropeLoop.SetParent(this);
    }

    private void TryUpdateRope()
    {
        ChangeRopeLenght(playerA.PlayerAttackManager.currentCharge, playerB.PlayerAttackManager.currentCharge);
    }

    public void RemoveRope()
    {
        currentPullDistance = 9999f;
        rope.gameObject.SetActive(false);
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

    public void ChargeBothPlayers(float value)
    {
        playerA.PlayerAttackManager.ChangeCharge(value);
        playerB.PlayerAttackManager.ChangeCharge(value);
    }
}
