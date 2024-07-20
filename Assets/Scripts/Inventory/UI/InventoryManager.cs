using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private DeleteButton _deleteButton;

    private Inventory _inventory;
    private InventorySlot[] _slots;
    private Stack _selectedStack;

    private void Awake()
    {
        _slots = GetComponentsInChildren<InventorySlot>();
        if (_slots == null || _slots.Length <= 0)
            throw new ArgumentException("Среди дочерних объектов нет слотов инвентаря.");
        _inventory = new(_slots.Length);
        UpdateUI();
        gameObject.SetActive(false);
        _inventory.InventoryUpdated += UpdateUI;
    }

    public void UpdateUI()
    {
        _inventory.ResolveStacks();
        List<Stack> items = _inventory.GetAllItems();

        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < items.Count)
            {
                var item = items[i];
                _slots[i].SetItem(item);
                _slots[i].HighlightSelection(item == _selectedStack);
            }
            else
            {
                _slots[i]?.ClearSlot();
            }
        }
    }

    public void AddItem(Item item, int quantity)
    {
        _inventory.AddItem(item, quantity);
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        _inventory.RemoveItem(item, quantity);
    }

    public void RemoveStack(Stack stack)
    {
        _inventory.RemoveStack(stack);
    }

    public void SelectItem(Stack item)
    {
        _selectedStack = item;
        _deleteButton.gameObject.SetActive(true);
        UpdateUI();
    }

    public void ClearSelectedItem()
    {
        _selectedStack = null;
        _deleteButton.gameObject.SetActive(false);
        UpdateUI();
    }

    public void DeleteSelectedItem()
    {
        ClearSelectedItem();
        RemoveStack(_selectedStack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClearSelectedItem();
    }
}
