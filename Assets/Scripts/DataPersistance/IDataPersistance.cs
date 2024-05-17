
// INTERFACE THAT MARKS OBJECTS THAT REQUIRE INFORMATION FROM SAVED GAME STATE
// OR POSSES DATA THAT NEEDS TO BE SAVED

public interface IDataPersistance
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
