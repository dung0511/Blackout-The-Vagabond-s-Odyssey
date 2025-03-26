using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    public bool SaveLocal<T>(string savePath, T Data)
    {
        var relativePath = Application.persistentDataPath + savePath;  //relative path for different machine systems
        try
        {
            if(File.Exists(relativePath))
            {
                Debug.Log("Save file existed, replacing..");
                File.Delete(relativePath);
            } else {
                Debug.Log("Creating save file...");
            }
            using FileStream stream = File.Create(relativePath);
            stream.Close();
            File.WriteAllText(relativePath, JsonConvert.SerializeObject(Data));
            return true;
        } catch (Exception e)
        {
            Debug.LogError($"Save failed: {e.Message}\n{e.StackTrace}");
            return false;
        }
        
    }

    public bool LoadLocal<T>(string loadPath)
    {
        throw new System.NotImplementedException();
    }

}
