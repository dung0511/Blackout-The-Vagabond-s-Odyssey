using System;
using System.Diagnostics;

[Serializable]
public static class DataManager
{
    public static PlayerData playerData = new();

    public static void Save()
    {
        SaveSystem.SaveLocal("/save.sav",playerData);
    }

    public static void Load()
    {
       playerData =  SaveSystem.LoadLocal<PlayerData>("/save.sav");
    }
}

