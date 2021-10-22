using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private Enemy _template;
    [SerializeField, Range(0, 1)] private float _spawnChance;
    [SerializeField] private LevelRestarter _levelRestarter;
     
    private bool _isCanDisable = true;

    public float SpawnChance => _spawnChance;

    private void Awake()
    {
        Initialize(_template.gameObject);
    }

    private void OnEnable()
    {
        _levelRestarter.RestartFinished += OnRestartFinished;
    }

    private void OnDisable()
    {
        _levelRestarter.RestartFinished -= OnRestartFinished;
    }

    public void SpawnAt(Vector2 position)
    {
        if (TryGetObject(out GameObject enemy))
        {
            enemy.transform.position = position;
            enemy.SetActive(true);
        }

        if (_isCanDisable)
        {
            DisableObjectOutsideScreen();
        }
    }

    public override void ResetPool()
    {
        base.ResetPool();
        _isCanDisable = false;
    }

    private void OnRestartFinished()
    {
        _isCanDisable = true;
    }
}
