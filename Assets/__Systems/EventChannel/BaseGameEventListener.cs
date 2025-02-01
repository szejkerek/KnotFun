using UnityEngine;
using UnityEngine.Events;

namespace PlaceHolders.EventChannel
{
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> 
        where E: BaseGameEvent<T> 
        where UER: UnityEvent<T>
    {

        public E GameEvent => gameEvent;
        [SerializeField] E gameEvent;

        public UER UnityEventResponse => unityEventResponse;
        [SerializeField] UER unityEventResponse;


         void OnEnable()
        {
            if (gameEvent == null)
                return;

            GameEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            if (gameEvent == null)
                return;

            GameEvent.UnregisterListener(this);
        }


        public void OnEventRaised(T item)
        {
            if (unityEventResponse == null)
                return;

            unityEventResponse.Invoke(item);
        }
    }
}
