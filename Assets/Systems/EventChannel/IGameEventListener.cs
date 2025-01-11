namespace PlaceHolders.EventChannel
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
