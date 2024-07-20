using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Collectable : MonoBehaviour
{
    [SerializeField]
    private Item _item;
    [SerializeField]
    private int _count;

    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnDrawGizmos()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _item.Icon;
    }


    public void Collect(InventoryManager inventoryManager)
    {
        inventoryManager.AddItem(_item, _count);
        Destroy(gameObject);
    }
}
