using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [Range(0f,1f)] public float currentCharge;
    
    public bool TryDischarge(float value)
    {
        if (currentCharge - value <= 0)
        {
            return false;
        }
        currentCharge -= value;
        currentCharge = Mathf.Clamp01(currentCharge);
        return true;
    }

    public void ChangeCharge(float value)
    {
        currentCharge = Mathf.Clamp01(currentCharge + value);
    }
}
