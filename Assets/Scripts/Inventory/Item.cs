using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField]
    private string _id;
    [SerializeField]
    private string _itemName;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private int _maxStackSize = 1;

    public string Id { get => _id; }
    public string ItemName { get => _itemName; }
    public Sprite Icon { get => _icon; }
    public int MaxStackSize { get => _maxStackSize; }
}
