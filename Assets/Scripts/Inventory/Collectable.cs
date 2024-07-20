using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Collectable : MonoBehaviour
{
    [SerializeField]
    private Item _item;
    [SerializeField]
    private int _count;

    private SpriteRenderer _spriteRenderer;

    public Item Item { get => _item; }
    public int Count { get => _count; }

    public event Action<Collectable> CollectItem;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _item.Icon;
    }


    private void OnDrawGizmos()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _item.Icon;
    }

    public void Create(Item item, int count)
    {
        if(_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        _item = item;
        _spriteRenderer.sprite = _item.Icon;
        _count = count;
        GameManager.Instance.OnLootCreated(this);
    }

    public void Collect(InventoryManager inventoryManager)
    {
        inventoryManager.AddItem(_item, _count);
        CollectItem?.Invoke(this);
        Destroy(gameObject);
    }
}
