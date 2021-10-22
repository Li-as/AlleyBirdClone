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
    [SerializeField] private LevelRestarter _levelRestarter;

    private float _passedTime;
    private Vector2 _nextSpawnPosition;
    private int _withoutObjectsLeft;
    private bool _isCanDisable = true;

    public float WithoutObjectsSpawnChance => _withoutObjectsSpawnChance;

    private void OnEnable()
    {
        _levelRestarter.RestartFinished += OnRestartFinished;
    }

    private void OnDisable()
    {
        _levelRestarter.RestartFinished -= OnRestartFinished;
    }

    private void Start()
    {
        Initialize(_template.gameObject);
        _nextSpawnPosition = _startSpawnPosition;
        _withoutObjectsLeft = _withoutObjectsAmount;
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

                if (_withoutObjectsLeft > 0)
                {
                    _withoutObjectsLeft--;
                    continue;
                }

                _onPlatformObjectSpawner.SpawnOn(platform, _platformHeight, _distanceBetweenPlatforms);
            }

            if (_isCanDisable)
            {
                DisableObjectOutsideScreen();
            }
        }
    }

    public override void ResetPool()
    {
        base.ResetPool();
        _passedTime = 0;
        _nextSpawnPosition = _startSpawnPosition;
        _withoutObjectsLeft = _withoutObjectsAmount;
        _isCanDisable = false;
        Spawn(_startSpawnAmount);
    }

    private void OnRestartFinished()
    {
        _isCanDisable = true;
    }
}
