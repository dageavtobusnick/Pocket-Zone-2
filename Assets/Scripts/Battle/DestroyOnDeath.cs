using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class DestroyOnDeath: MonoBehaviour
{
    private HP _hp;

    private void Awake()
    {
        _hp = GetComponent<HP>();
    }

    private void OnEnable()
    {
        _hp.Dead += OnDeath;
    }

    private void OnDisable()
    {
        _hp.Dead -= OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
