using System;
using System.Diagnostics;

[Serializable]
public static class DataManager
{
    public static GameData gameData = new();

    public static void Save()
    {
        SaveSystem.SaveLocal("/save.sav",gameData);
    }

    public static void Load()
    {
       gameData =  SaveSystem.LoadLocal<GameData>("/save.sav");
    }
}

public class GameData 
{
    public PlayerData playerData = new();
    //more ?
}