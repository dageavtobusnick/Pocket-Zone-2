using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Zone : MonoBehaviour
{
    private CircleCollider2D _circleCollider;
    private Transform _target;

    public Transform Target { get => _target; }

    public void SetZone(Transform target, float radius)
    { 
        if(_circleCollider == null)
            _circleCollider = GetComponent<CircleCollider2D>();
        _target = target; 
        _circleCollider.radius = radius;
    }   
    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

}
