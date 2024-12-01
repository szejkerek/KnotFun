using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private Player player;
    public float dischargeSpeed = 2f;
    
    public Action OnChargeChanged;
    [Range(0f,1f)] public float currentCharge;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.gameDevice == GameDevice.Keyboard && 
            player.TriggerHeld(GameDevice.Keyboard))
        {
            TryDischarge(dischargeSpeed*Time.deltaTime);
        }
        
        if (player.gameDevice == GameDevice.Pad1 && 
            player.TriggerHeld(GameDevice.Pad1))
        {
            TryDischarge(dischargeSpeed*Time.deltaTime);
        }
        
        if (player.gameDevice == GameDevice.Pad2 && 
            player.TriggerHeld(GameDevice.Pad2))
        {
            TryDischarge(dischargeSpeed*Time.deltaTime);
        }
    }

    public bool TryDischarge(float value)
    {
        if (currentCharge - value <= 0)
        {
            return false;
        }
        ChangeCharge(-value);
        return true;
    }

    public void ChangeCharge(float value)
    {
        currentCharge = Mathf.Clamp01(currentCharge + value);
        OnChargeChanged?.Invoke();
    }
}
