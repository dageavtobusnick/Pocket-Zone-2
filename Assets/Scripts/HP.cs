using System;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private Team _team;

    private int _health;

    public int MaxHealth { get => _maxHealth; }
    public Team Team { get => _team; }

    public event Action<int> HealthChange;
    public event Action Dead;

    private void Awake()
    {
        _health = _maxHealth;
    }

    private void Start()
    {
        HealthChange?.Invoke(_health);
    }

    public void TakeDamage(int health)
    {
        _health -= health;
        if(_health <= 0)
            Dead?.Invoke();
        HealthChange?.Invoke(_health);
    }
}
