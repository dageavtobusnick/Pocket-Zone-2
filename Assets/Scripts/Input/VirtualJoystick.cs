using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform _background;
    private RectTransform _handle;
    private Vector2 _inputVector;

    private void Start()
    {
        _background = GetComponent<RectTransform>();
        _handle = _background.GetChild(0).GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, eventData.position, eventData.pressEventCamera, out Vector2 position))
        {
            position.x /= _background.sizeDelta.x;
            position.y /= _background.sizeDelta.y;

            _inputVector = new Vector2(position.x * 2 + 1, position.y * 2 - 1);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            // Move Handle
            _handle.anchoredPosition = new Vector2(_inputVector.x * (_background.sizeDelta.x / 3), _inputVector.y * (_background.sizeDelta.y / 3));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return _inputVector.x;
    }

    public float Vertical()
    {
        return _inputVector.y;
    }

    public Vector2 Direction()
    {
        return new Vector2(Horizontal(), Vertical());
    }
}
