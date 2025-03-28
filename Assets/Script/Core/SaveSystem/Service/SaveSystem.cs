using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveSystem
{
    public static bool SaveLocal<T>(string savePath, T Data)
    {
        var relativePath = Application.persistentDataPath + savePath;  //relative path for different machine systems
        var backupPath = Path.ChangeExtension(relativePath, ".bak");
        try
        {
            if(File.Exists(relativePath))
            {
                Debug.Log("Save file existed, creating a backup...");
                if (File.Exists(backupPath)) //delete previous backup
                {
                    File.Delete(backupPath);
                }
                File.Copy(relativePath, backupPath); // Backup the existing save
            } else {
                Debug.Log("Creating save file...");
            }
            using FileStream stream = File.Create(relativePath);
            stream.Close();
            File.WriteAllText(relativePath, JsonConvert.SerializeObject(Data, Formatting.Indented));
            Debug.Log(relativePath);
            return true;
        } catch (Exception e)
        {
            Debug.LogError($"Save failed: {e.Message}\n{e.StackTrace}");
            return false;
        }
    }

    public static T LoadLocal<T>(string loadPath)
    {
        var relativePath = Application.persistentDataPath + loadPath;
        if(!File.Exists(relativePath))
        {
            throw new FileNotFoundException($"Load failed: No save file at {relativePath}");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(relativePath));
            if(data == null) throw new NullReferenceException("data null");
            Debug.Log("Load success");
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e.Message}\n{e.StackTrace}");
            throw e;
        }
    }

    public static T LoadLocal<T>(T objToMod, string loadPath)
    {
        var relativePath = Path.Combine(Application.persistentDataPath, loadPath);
        if(!File.Exists(relativePath))
        {
            throw new FileNotFoundException($"Load failed: No save file at {relativePath}");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(relativePath));
            Debug.Log("Load success");
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e.Message}\n{e.StackTrace}");
            throw e;
        }
    }

}
