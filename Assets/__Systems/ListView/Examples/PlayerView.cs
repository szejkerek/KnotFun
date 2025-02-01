using PlaceHolders.ListView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : ViewPrefab<PlayerModel>
{
    [SerializeField] Image image;
    public override void SetData(PlayerModel data)
    {
        image.color = data.color;
    }
}
