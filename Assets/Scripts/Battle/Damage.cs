using System;
using UnityEngine;

public class Damage : MonoBehaviour
{
    protected bool DestroyOnCollision;

    [SerializeField]
    private HP _hP;

    private int _damage = 0;
    private bool _isDamageAllowed = false;

    public event Action DamageGiven;

    public void SetDamage(int damage)
    {
        if(damage < 0) 
            _damage = 0;
        else
            _damage = damage;
    }

    public void ExtractTeam(HP hP)
    {
        _hP = hP;
    }


    public void AllowDamage() => _isDamageAllowed = true;

    public void DenyDamage() => _isDamageAllowed = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isDamageAllowed &&
            other.TryGetComponent<HitBox>(out var hitBox) &&
            hitBox.TakeDamage(_damage, _hP.Team))
        {
            DamageGiven?.Invoke();
        }
    }
}
