using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public Action OnChargeChanged;
    public int id;
    [Range(0f,1f)] public float currentCharge;

    private void Update()
    {
        if (Input.GetKey(KeyCode.B) && id == 1)
        {
            TryDischarge(0.1f*Time.deltaTime);
            Debug.Log("B");
        }
        
        if (Input.GetKey(KeyCode.N) && id == 2)
        {
            TryDischarge(0.1f*Time.deltaTime);
            Debug.Log("N");
        }
        
        if (Input.GetKey(KeyCode.M) && id == 3)
        {
            TryDischarge(0.1f*Time.deltaTime);
            Debug.Log("M");
        }
    }

    public bool TryDischarge(float value)
    {
        if (currentCharge - value <= 0)
        {
            return false;
        }
        ChangeCharge(value);
        return true;
    }

    public void ChangeCharge(float value)
    {
        currentCharge = Mathf.Clamp01(currentCharge + value);
        OnChargeChanged?.Invoke();
    }
}
