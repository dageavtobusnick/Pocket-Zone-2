using System;

[Serializable]
public class StackData
{
    private readonly string _id;
    private readonly int _count;

    public string Id => _id;
    public int Count => _count;

    public StackData(string id, int count)
    {
        _id = id;
        _count = count;
    }
}

