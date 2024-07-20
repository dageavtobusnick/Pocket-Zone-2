using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class StartAttackZone : Zone
{
    public event Action AttackStarted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HitBox>(out var _))
        {
            AttackStarted?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<HitBox>(out var _))
        {
            AttackStarted?.Invoke();
        }
    }
}

