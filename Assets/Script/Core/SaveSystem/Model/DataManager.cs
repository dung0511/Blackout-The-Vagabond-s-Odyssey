using System;

[Serializable]
public static class DataManager
{
    public static PlayerData playerData;

    public static void Save()
    {
        SaveSystem.SaveLocal("save.sav",playerData);
    }
}