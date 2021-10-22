using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover), typeof(PlayerCollisionHandler))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private PlayerCollisionHandler _collisionHandler;
    private int _score;
    private int _coins;

    public event UnityAction<int> ScoreChanged;
    public event UnityAction<int> CoinsChanged;
    public event UnityAction GameOver;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
    }

    public void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void IncreaseCoins()
    {
        _coins++;
        CoinsChanged?.Invoke(_coins);
    }

    public void Die()
    {
        GameOver?.Invoke();
    }

    public void ResetPlayer()
    {
        _score = 0;
        _coins = 0;
        ScoreChanged?.Invoke(_score);
        CoinsChanged?.Invoke(_coins);
        _mover.ResetState();
        _collisionHandler.ResetCollision();
    }
}
