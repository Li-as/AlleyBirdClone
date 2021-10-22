using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private Enemy _template;
    [SerializeField, Range(0, 1)] private float _spawnChance;

    public float SpawnChance => _spawnChance;

    private void Awake()
    {
        Initialize(_template.gameObject);
    }

    public void SpawnAt(Vector2 position)
    {
        if (TryGetObject(out GameObject enemy))
        {
            enemy.transform.position = position;
            enemy.SetActive(true);
        }
    }
}
