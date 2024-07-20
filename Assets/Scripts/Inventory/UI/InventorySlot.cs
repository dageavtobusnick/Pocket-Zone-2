using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _quantityText;

    [Inject]
    private InventoryManager _inventoryManager;

    private Stack _stack;
    private int _quantity;

    private event Action<Stack> Selected;

    public void SetItem(Stack newStack)
    {
        _stack = newStack;
        _quantity = _stack.Count;

        _icon.sprite = _stack.Item.Icon;
        _icon.enabled = true;
        _quantityText.text = _quantity > 1 ? _quantity.ToString() : "";
        Selected += _inventoryManager.SelectItem;
    }

    public void ClearSlot()
    {
        _stack = null;
        _quantity = 0;

        _icon.sprite = null;
        _icon.enabled = false;
        _quantityText.text = "";
        Selected -= _inventoryManager.SelectItem;
    }

    public void HighlightSelection(bool highlight)
    {
        _icon.color = highlight ? Color.yellow : Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected?.Invoke(_stack);
    }
}
