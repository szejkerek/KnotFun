
public interface ISavable
{
    string SavedFilename { get; }
    void Load();
    void Save();
}
