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

    private float _passedTime;
    private Vector2 _nextSpawnPosition;

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
            }
        }
    }
}