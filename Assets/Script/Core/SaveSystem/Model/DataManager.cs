using System;
using System.Diagnostics;

[Serializable]
public static class DataManager
{
    public static GameData gameData = new();

    public static void Save()
    {
        SaveSystem.SaveLocal("/save.sav",gameData, true);
    }

    public static void Load()
    {
        gameData =  SaveSystem.LoadLocal<GameData>("/save.sav", true);
        if (gameData == null)
        {
            gameData = new();
        }
    }
}

public class GameData 
{
    public PlayerData playerData = new();
    //more ?
}