using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders.EventChannel
{
    public class BaseGameEvent<T> : ScriptableObject
    {
        public readonly List<IGameEventListener<T>> eventListeners = new();

        public void Raise(T item)
        {
            for (int i = eventListeners.Count - 1; i >= 0 ; i--)
            {
                eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (eventListeners.Contains(listener))
                return;

            eventListeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
                return;

            eventListeners.Remove(listener);
        }
    }
}
