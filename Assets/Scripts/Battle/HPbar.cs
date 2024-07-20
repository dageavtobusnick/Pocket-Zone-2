using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HPbar : MonoBehaviour
{
    [SerializeField]
    private HP _hP;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _hP.MaxHealth;
    }

    private void OnEnable()
    {
        _hP.HealthChange += OnHealthChanged;
    }

    private void OnDisable()
    {
        _hP.HealthChange -= OnHealthChanged;

    }

    private void OnHealthChanged(int health)
    {
        _slider.value = health;
    }
}
