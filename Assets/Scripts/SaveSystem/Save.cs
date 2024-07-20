using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    [SerializeField]
    private PlayerData _playerData;
    [SerializeField]
    public List<MobData> _mobData;
    [SerializeField]
    public List<LootData> _lootData;
    public PlayerData PlayerData { get => _playerData; }
    public List<MobData> MobData { get => _mobData; }
    public List<LootData> LootData { get => _lootData; }

    public Save(PlayerData playerData, List<MobData> mobData, List<LootData> lootData)
    {
        _playerData = playerData;
        _mobData = mobData;
        _lootData = lootData;
    }
}
