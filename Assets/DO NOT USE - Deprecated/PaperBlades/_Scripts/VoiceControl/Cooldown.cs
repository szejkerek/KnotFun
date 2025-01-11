using UnityEngine;

[System.Serializable]
public class Cooldown
{
    public float cooldowTime;
    float last;

    public Cooldown()
    {
        last = 0;
    }

    public void ResetTimers(bool setBehind = false)
    {
        if (setBehind)
        {
            last = Time.time - cooldowTime;
        }
        else
        {
            last = Time.time;
        }
    }

    public bool IsOffCooldown()
    {
        if (ReadyToUseRatio() == 1 || last > Time.time)
        {
            return true;
        }
        return false;
    }

    public float ReadyToUseRatio()
    {
        float timeSinceLastCommand = Time.time - last;
        float ratio = Mathf.Clamp01(timeSinceLastCommand / cooldowTime);
        return ratio;
    }
}