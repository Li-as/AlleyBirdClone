using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : ObjectPool
{
    [SerializeField] private Platform _template;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private float _distanceBetweenPlatforms;
    [SerializeField] private Vector2 _startSpawnPosition;
    [SerializeField] private int _startSpawnAmount;
    [SerializeField] private int _withoutObjectsAmount;
    [SerializeField, Range(0, 1)] private float _withoutObjectsSpawnChance;
    [SerializeField] private float _platformHeight;
    [SerializeField] private OnPlatformObjectSpawner _onPlatformObjectSpawner;

    private float _passedTime;
    private Vector2 _nextSpawnPosition;

    public float WithoutObjectsSpawnChance => _withoutObjectsSpawnChance;

    private void Start()
    {
        Initialize(_template.gameObject);
        _nextSpawnPosition = _startSpawnPosition;
        Spawn(_startSpawnAmount);
    }

    private void Update()
    {
        if (_passedTime >= _secondsBetweenSpawn)
        {
            Spawn(1);
        }

        _passedTime += Time.deltaTime;
    }

    private void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (TryGetObject(out GameObject platform))
            {
                platform.transform.position = _nextSpawnPosition;
                platform.SetActive(true);
                _nextSpawnPosition.y += _distanceBetweenPlatforms;
                _passedTime = 0;

                if (_withoutObjectsAmount > 0)
                {
                    _withoutObjectsAmount--;
                    continue;
                }

                _onPlatformObjectSpawner.SpawnOn(platform, _platformHeight, _distanceBetweenPlatforms);
            }
        }
    }
}
