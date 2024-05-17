using System;
using System.IO;
using UnityEngine;

// CLASS THAT HANDLES PERSISTING DATA TO A FILE IN JSON FORMAT,
// LOADING DATA FROM THAT FILE AND ENCRYPTING DATA

// NOTE: IT USES Application.PersistanceDataPath AS dataDirPath by default
// WHICH RESOLVES TO C:\Users\{USER}\AppData\LocalLow\{COMPANY_NAME}\{PROJECT_NAME} ON WINDOWS

public class FileDataHandler
{
    private readonly string dataDirPath = "";
    private readonly string dataFilePath = "";
    private readonly bool useEncryption = false;
    private readonly string encryptionCodeWord = "moblieGame";

    public FileDataHandler(string dataDirPath, string dataFilePath, bool useEncryption) {
        this.dataDirPath = dataDirPath;
        this.dataFilePath = dataFilePath;
        this.useEncryption = useEncryption;
    }

    public GameData Load() {

        string fullPath = Path.Combine(dataDirPath, dataFilePath);
        GameData loadedData = null;

        if(File.Exists(fullPath)) {
            try {
                string json = "";
                // LOAD DATA AS JSON FROM FILE
                using(FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using(StreamReader reader = new StreamReader(stream)) {
                        json = reader.ReadToEnd();
                    }
                }
                // OPTIONALLY DECRYPT DATA
                if(useEncryption) {
                    json = EncryptDecrypt(json);
                }
                // DESERIALIZE DATA
                loadedData = JsonUtility.FromJson<GameData>(json);
            }
            catch(Exception e) {
                Debug.LogError("Error occurred while saving data to file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData gameData) {

        string fullPath = Path.Combine(dataDirPath, dataFilePath);

        try {
            // CREATE DIRECTORY
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // SERIALIZE GAME DATA
            string json = JsonUtility.ToJson(gameData, true);
            // OPTIONALLY ENCRYPT DATA
            if(useEncryption) {
                json = EncryptDecrypt(json);
            }
            // WRITE DATA TO FILE
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                using(StreamWriter writer = new StreamWriter(stream)) {
                    writer.Write(json);
                }
            }
        }
        catch(Exception e) {
            Debug.LogError("Error occurred while saving data to file: " + fullPath + "\n" + e);
        }
    }

    // ENCRYPTS AND DECRYPTS USING XOR ALGORITHM
    private string EncryptDecrypt(string data) {

        string modifiedData = "";
        for(int i = 0; i < data.Length; i++) {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
