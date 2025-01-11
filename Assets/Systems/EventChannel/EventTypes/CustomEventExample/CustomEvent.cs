using UnityEngine;

namespace PlaceHolders.EventChannel
{
    [CreateAssetMenu(fileName = "New Custom Event", menuName = "Game Events/Custom Event")]
    public class CustomEvent : BaseGameEvent<CustomDataStructure>
    {
    }
}
