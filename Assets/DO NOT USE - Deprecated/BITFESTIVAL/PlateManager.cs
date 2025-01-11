using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public List<PressurePlates> plates = new List<PressurePlates>();


    private void Update()
    {
        if (AllActive()) Debug.Log("WON");
    }

    private bool AllActive()
    {
        for (int i = 0; i < plates.Count; i++)
        {
            if (!plates[i].activated) return false;
        }

        return true;
    }

}
