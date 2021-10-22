using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlatformObjectSpawner : MonoBehaviour
{
    [SerializeField] private PlatformSpawner _platformSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private float _spawnOffsetFromScreenEdges;
    [SerializeField] private float _maxDistanceBetweenObjects;

    private float _possibleXSpawnFrom;
    private float _possibleXSpawnTo;

    private void Awake()
    {
        _possibleXSpawnFrom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        _possibleXSpawnFrom += _spawnOffsetFromScreenEdges;
        _possibleXSpawnTo = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        _possibleXSpawnTo -= _spawnOffsetFromScreenEdges;
    }

    public void SpawnOn(GameObject platform, float platformHeight, float distanceBetweenPlatforms)
    {
        if (Random.Range(0, 1f) < _platformSpawner.WithoutObjectsSpawnChance)
        {
            return;
        }
        else
        {
            if (Random.Range(0, 1f) < _enemySpawner.SpawnChance)
            {
                float spawnPositionX = Random.Range(_possibleXSpawnFrom, _possibleXSpawnTo);
                float spawnPositionY = platform.transform.position.y + platformHeight / 2;
                _enemySpawner.SpawnAt(new Vector2(spawnPositionX, spawnPositionY));

                if (Random.Range(0, 1f) < _coinSpawner.SpawnChance)
                {
                    float coinSpawnPositionX = Random.Range(_possibleXSpawnFrom, _possibleXSpawnTo);
                    float coinSpawnPositionY = Random.Range(platform.transform.position.y + platformHeight / 2, platform.transform.position.y + distanceBetweenPlatforms - platformHeight / 2);
                    if (Mathf.Abs(coinSpawnPositionX - spawnPositionX) < _maxDistanceBetweenObjects)
                    {
                        coinSpawnPositionX = spawnPositionX;
                        coinSpawnPositionY = platform.transform.position.y + distanceBetweenPlatforms - platformHeight / 2;
                    }
                    _coinSpawner.SpawnAt(new Vector2(coinSpawnPositionX, coinSpawnPositionY));
                }
            }
            else if (Random.Range(0, 1f) < _coinSpawner.SpawnChance)
            {
                float spawnPositionX = Random.Range(_possibleXSpawnFrom, _possibleXSpawnTo);
                float spawnPositionY = Random.Range(platform.transform.position.y + platformHeight / 2, platform.transform.position.y + distanceBetweenPlatforms - platformHeight / 2);
                _coinSpawner.SpawnAt(new Vector2(spawnPositionX, spawnPositionY));
            }
        }
    }
}
