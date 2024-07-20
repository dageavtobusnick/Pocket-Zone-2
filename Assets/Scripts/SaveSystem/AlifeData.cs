using System;

[Serializable]
public class AlifeData:Data
{
    private int _hp;
    public int HP { get => _hp; }
    public AlifeData(float x, float y, int hP) : base(x, y)
    {
        _hp = hP;
    }
}
