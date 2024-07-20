using System;
using UnityEngine;
public class ZoneOfView : Zone
{
    public event Action TargetInZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HitBox>(out var _))
        {
            TargetInZone?.Invoke();
        }
    }

}
