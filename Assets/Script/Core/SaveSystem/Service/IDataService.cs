public interface IDataService
{
    public bool SaveLocal<T>(string savePath, T Data);
    public bool LoadLocal<T>(string loadPath);
}
