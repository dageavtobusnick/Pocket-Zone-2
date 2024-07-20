using System;

[Serializable]
public class LootData: Data
{
    private string _id;
    private int _count;
    public string Id { get => _id; }
    public int Count { get => _count; }

    public LootData(string id, float x, float y, int count):base(x, y)
    {
        _id = id;
        _count = count;
    }
}
