using UnityEngine;

public class MapBounds
{
    public Vector2 MinBounds { get; }
    public Vector2 MaxBounds { get; }

    public MapBounds(Vector2 minBounds, Vector2 maxBounds)
    {
        MinBounds = minBounds;
        MaxBounds = maxBounds;
    }
}

