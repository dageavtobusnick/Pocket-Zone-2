using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Registry", menuName = "Inventory/ItemRegistry")]
public class ItemRegistry : ScriptableObject
{
    [SerializeField]
    private List<Item> items = new();

    public List<Item> Items { get => items; }
}
