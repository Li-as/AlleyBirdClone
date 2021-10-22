using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelRestarter : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] CoinSpawner _coinSpawner;
    [SerializeField] PlatformSpawner _platformSpawner;
    [SerializeField] private float _restartDelay;

    public event UnityAction RestartFinished;

    public void Restart()
    {
        _enemySpawner.ResetPool();
        _coinSpawner.ResetPool();
        _platformSpawner.ResetPool();
        _player.ResetPlayer();

        StartCoroutine(RestartRoutine(_restartDelay));
    }

    private IEnumerator RestartRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartFinished?.Invoke();
    }
}
