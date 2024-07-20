using UnityEngine;

[RequireComponent (typeof(HP))]
public class MobLoader : MonoBehaviour, IDataLoader<MobData>
{
    private HP _hP;

    private void Awake()
    {
        _hP = GetComponent<HP>();
    }
    public void LoadData(MobData data)
    {
        _hP.LoadHealth(data);
        transform.position = new Vector3(data.X, data.Y, 0);
    }

    public MobData SaveData()
    {
        var position = transform.position;
        return new MobData(position.x, position.y, _hP.Health);
    }
}

