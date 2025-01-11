using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour
{
    List<Player> playersIn;
    public bool activated = false;
    private void OnTriggerEnter(Collider other)
    {
        Player p;
        if(other.gameObject.TryGetComponent<Player>(out p))
        {
            if(!playersIn.Contains(p))
            playersIn.Add(p);
        }
        if (playersIn.Count > 0) activated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Player p;
        if (other.gameObject.TryGetComponent<Player>(out p))
        {
            if (playersIn.Contains(p))
                playersIn.Remove(p);
        }
        if (playersIn.Count == 0) activated = false;
    }
}
