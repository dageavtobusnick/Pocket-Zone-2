using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData: AlifeData
{
    [SerializeField]
    private List<StackData> _inventory;
    public List<StackData> Inventory { get => _inventory; }

    public PlayerData(float x, float y, int hP, List<StackData> inventory): base(x, y, hP)
    {
        _inventory = inventory;
    }
}
