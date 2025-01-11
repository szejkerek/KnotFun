using System;
using UnityEngine;

public class RopeLoop : MonoBehaviour
{
    RopeManager rope;
    public void SetParent(RopeManager ropeManager)
    {
        rope = ropeManager;
    }

    public void ChargeRope(float value)
    {
        rope.ChargeBothPlayers(value);
    }
}
