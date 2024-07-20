public interface IDataLoader<T>
{
    void LoadData(T data);
    T SaveData();
}

