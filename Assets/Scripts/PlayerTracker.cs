using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _maxDistanceFromPlayerY;
    [SerializeField] private float _trackSpeed;
    [SerializeField] private LevelStarter _levelStarter;

    private bool _isShouldTrack;

    private void OnEnable()
    {
        _levelStarter.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _levelStarter.LevelStarted -= OnLevelStarted;
    }

    private void Update()
    {
        if (_isShouldTrack)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, _player.transform.position.y - _yOffset, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _trackSpeed);

            if (Mathf.Abs(transform.position.y - _player.transform.position.y) > _maxDistanceFromPlayerY)
            {
                transform.position = new Vector3(transform.position.x, _player.transform.position.y + _maxDistanceFromPlayerY, transform.position.z);
            }
        }
    }

    private void OnLevelStarted()
    {
        _isShouldTrack = true;
    }
}
