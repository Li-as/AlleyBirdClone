using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 _startPosition;

    private PlayerMover _mover;
    private int _score;

    public Vector2 StartPosition => _startPosition;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction GameOver;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
    }

    public void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void Die()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
        GameOver?.Invoke();
    }

    public void ResetPlayer()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _mover.ResetState();
    }
}
