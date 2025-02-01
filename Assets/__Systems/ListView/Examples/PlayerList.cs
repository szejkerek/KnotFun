using PlaceHolders.ListView;
using UnityEngine;

public class PlayerList : ListView<PlayerModel> 
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Items.Add(new PlayerModel());
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            Items.RemoveAt(0);
        }        
        
        if (Input.GetKeyDown(KeyCode.F7))
        {
            Items.RemoveAt(Items.Count - 1);
        }
    }
}
