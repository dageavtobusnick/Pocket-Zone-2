using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collectable))]
public class LootLoader : MonoBehaviour, IDataLoader<LootData>
{
    [SerializeField]
    private ItemRegistry items;

    private Collectable collectable;

    private void Awake()
    {
        collectable = GetComponent<Collectable>();
    }

    public void LoadData(LootData data)
    {
        if (collectable == null)
        {
            collectable = GetComponent<Collectable>();
        }
        collectable.Create(items.Items.First(x=> x.Id == data.Id), data.Count);
        transform.position = new Vector3(data.X, data.Y, 0);
    }

    public LootData SaveData()
    {
        var position = transform.position;
        if (collectable == null)
        {
            collectable = GetComponent<Collectable>();
        }
        return new LootData(collectable.Item.Id, position.x, position.y, collectable.Count);
    }
}
