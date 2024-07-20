using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PlayerController _player;
    [SerializeField]
    private List<Item> _possibleItems;
    [SerializeField]
    private List<EnemyAI> _possibleEnemies;
    [SerializeField]
    private List<Transform> _enemySpawns;
    [SerializeField]
    private List<Transform> _collectablesSpawn;
    [SerializeField]
    private Collectable collectable;
    [SerializeField]
    private int _enemiesCount;
    [SerializeField]
    private int _collectablesCount;
    [SerializeField]
    private int _lootCount;
    [SerializeField]
    private int _saveTimeout;

    private List<EnemyAI> _enemies = new ();
    private List<Collectable> _collectables = new ();
    private HP _playerHealth;
    private float _saveTime;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;
        if(!LoadData())
        {
            SpawnEnemies();
            SpawnLoot();
        }
        _playerHealth = _player.GetComponent<HP>();
    }


    private void Update()
    {
        _saveTime += Time.deltaTime;
        if( _saveTime > _saveTimeout)
        {
            Debug.Log("GameSaved");
            SaveData();
            _saveTime = 0;
        }
    }

    private void OnEnable()
    {
        _playerHealth.Dead += OnPlayerDied;
        foreach (var enemy in _enemies) 
        {
            enemy.Died += OnEnemyDied;
        }
        foreach (var loot in _collectables)
        {
            loot.CollectItem += OnLootCollected;
        }
    }

    private void OnDisable()
    {
        _playerHealth.Dead -= OnPlayerDied;
        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnEnemyDied;
        }
        foreach (var loot in _collectables)
        {
            loot.CollectItem -= OnLootCollected;
        }
    }

    public void OnEnemyDied(EnemyAI enemyAI)
    {
        _enemies.Remove(enemyAI.GetComponent<EnemyAI>());
        if(_enemies.Count <= 0)
        {
            RestartScene();
        }
    }

    public void OnPlayerDied()
    {
        RestartScene();
    }

    public void RestartScene()
    {
        SaveSystem.DeleteData();
        Scene currentScene = SceneManager.GetActiveScene();
        Instance = null;
        SceneManager.LoadScene(currentScene.name);
    }
    public void OnLootCreated(Collectable loot)
    {
        _collectables.Add(loot.GetComponent<Collectable>());
    }

    public void OnLootCollected(Collectable loot)
    {
        _collectables.Remove(loot);
    }

    public void SpawnEnemies()
    {
        for(var i = 0; i < _enemiesCount; i++)
        {
            var point = _enemySpawns[Random.Range(0, _enemySpawns.Count)];
            var enemy = _possibleEnemies[Random.Range(0, _possibleEnemies.Count)];
            var result = Instantiate(enemy, point.position, point.rotation);
            _enemies.Add(result);
        }
    }

    public void SpawnLoot()
    {
        for (var i = 0; i < _collectablesCount; i++)
        {
            var point = _collectablesSpawn[Random.Range(0, _collectablesSpawn.Count)];
            var item = _possibleItems[Random.Range(0, _possibleItems.Count)];
            var coolectables = Instantiate(collectable, point.position, point.rotation);
            collectable.GetComponent<Collectable>().Create(item,Random.Range(1, _lootCount));        }
    }

    public bool LoadData()
    {
        var data = SaveSystem.LoadData();
        if (data == null)
            return false;
        _player.GetComponent<IDataLoader<PlayerData>>().LoadData(data.PlayerData);
        foreach(var enemy in data.MobData)
        {
            var enemyObject = Instantiate(_possibleEnemies.First());
            enemyObject.GetComponent<IDataLoader<MobData>>().LoadData(enemy);
            _enemies.Add(enemyObject);
        }

        foreach (var loot in data.LootData)
        {
            var lootObject = Instantiate(collectable);
            lootObject.GetComponent<IDataLoader<LootData>>().LoadData(loot);
        }

        return true;

    }

    public async void SaveData()
    {
        var playerData = _player.GetComponent<IDataLoader<PlayerData>>().SaveData();
        var enemyData = new List<MobData>();
        var lootData = new List<LootData>();
        foreach (var enemy in _enemies)
        {
            enemyData.Add(enemy.GetComponent<IDataLoader<MobData>>().SaveData());
        }

        foreach (var loot in _collectables)
        {
            lootData.Add(loot.GetComponent<IDataLoader<LootData>>().SaveData());
        }
        await SaveSystem.SaveDataAsync(new Save(playerData, enemyData, lootData));
    }
}

