using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [Inject]
    private MapBounds _mapBounds;
    [Inject]
    private VirtualJoystick _joystick;

    private Rigidbody2D _rb;

    [Inject]
    public void Construct(MapBounds mapBounds, VirtualJoystick joystick)
    {
        _mapBounds = mapBounds;
        _joystick = joystick;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var moveInput = _joystick.Direction();
        var move = moveInput * _moveSpeed * Time.fixedDeltaTime;
        var newPosition = _rb.position + move;
        newPosition.x = Mathf.Clamp(newPosition.x, _mapBounds.MinBounds.x, _mapBounds.MaxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, _mapBounds.MinBounds.y, _mapBounds.MaxBounds.y);
        _rb.MovePosition(newPosition);
    }
}
