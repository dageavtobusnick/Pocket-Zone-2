﻿using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Inventory
{
    private List<Stack> _items = new();
    private int _slotsCount;

    public event Action InventoryUpdated;

    public Inventory(int slotsCount)
    {
        _slotsCount = slotsCount;
    }


    public void AddItem(Item item, int quantity = 1)
    {
        var newStack = new Stack(item, quantity);
        ResolveStacks(newStack);
        if (newStack.Count > 0 && _items.Count < _slotsCount)
        {
            _items.Add(newStack);
        }
        InventoryUpdated?.Invoke();
    }

    public void ResolveStacks(Stack newStack = null)
    {
        var stacksGroups = _items.GroupBy(x => x.Item);
        foreach(var stacksGroup in stacksGroups)
        {
            Stack oldStack = null;
            foreach (var stack in stacksGroup)
            {
                if (oldStack == null) 
                {
                    if (stack.Count > 0)
                        oldStack = stack;
                    else
                        _items.Remove(stack);
                    continue;
                }
                if (newStack != null && newStack.Item == oldStack.Item)
                    oldStack += newStack;
                oldStack += stack;

                if (stack.Count > 0)
                    oldStack = stack;
                else
                    _items.Remove(stack);
            }
            foreach(var stack in stacksGroup)
            {
                if(stack.Count <= 0)
                {
                    _items.Remove(stack);
                }
            }
        }
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        var stacks = _items.Where(x => x.Item == item);
        var tempQuantity = quantity;
        if (stacks.Any())
        {
            foreach (var stack in stacks)
            {
                if (stack.Count > tempQuantity)
                {
                    var temp = stack - quantity;
                    break;
                }
                else
                {
                    var temp = tempQuantity - stack.Count;
                    var temp_q = stack - tempQuantity;
                    tempQuantity = temp;
                }
            }
        }
        InventoryUpdated?.Invoke();
    }

    public void RemoveStack(Stack stack)
    {
        _items.Remove(stack);
        InventoryUpdated?.Invoke();
    }

    public int GetItemQuantity(Item item)
    {
        return _items.Where(x=>x.Item == item).Sum((x) => x.Count);
    }

    public bool ContainsItem(Item item)
    {
        return _items.Where(x => x.Item == item).Any();
    }

    public List<Stack> GetAllItems()
    {
        return _items;
    }

    public void SetSize(int size)
    {
        _slotsCount = size;
    }

    public List<StackData> GetData()
    {
        var result = new List<StackData>();
        foreach(var item in _items)
        {
            result.Add(item.GetData());
        }
        return result;
    }
}
