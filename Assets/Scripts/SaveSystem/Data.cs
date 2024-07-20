using System;

[Serializable]
public class Data
{
    private float _x;
    private float _y;
    public float X { get => _x; }
    public float Y { get => _y; }

    public Data(float x, float y)
    {
        _x = x;
        _y = y;
    }
}

