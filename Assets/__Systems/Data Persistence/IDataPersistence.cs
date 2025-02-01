
namespace PlaceHolders.DataPersistence
{
    public interface IDataPersistence
    {
        void CaptureState(GameData GameData);
        void RestoreState(GameData GameData);
    }

}