using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(1)] // SO IT IS CREATED AFTER ALL OBJECTS THAT MIGHT NEED DATA
public class DataManager : Singleton<DataManager>
{
    private List<IDataPersistance> dataPersistanceObjects;

    private void Start()
    {
        dataPersistanceObjects = FindAllDataPersistanceObjectsExceptAudioManager();
        LoadData();
    }

    public void LoadData()
    {
        foreach (IDataPersistance dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.LoadData(DataPersistanceManager.GameData);
        }
    }

    public void SaveData()
    {
        foreach (IDataPersistance dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.SaveData(ref DataPersistanceManager.GameData);
        }

        DataPersistanceManager.SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjectsExceptAudioManager()
    {
        // HERE DONT DESTROY ON LOAD OBJECTS SHOULD BE FILTERED OUT AS THEY ARE HANDLED IN DATA PERSISTANCE MANAGER
        IEnumerable<IDataPersistance> objects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>().Where(o => o is not AudioManager);
        return new List<IDataPersistance>(objects);
    }
}
