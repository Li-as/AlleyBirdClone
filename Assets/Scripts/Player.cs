using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover), typeof(PlayerCollisionHandler))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private PlayerMover _mover;
    private PlayerCollisionHandler _collisionHandler;
    private int _score;
    private int _coins;
    private int _bestScore;
    private int _totalCoins;

    public event UnityAction<int> ScoreChanged;
    public event UnityAction<int> BestScoreChanged;
    public event UnityAction<int> CoinsChanged;
    public event UnityAction<int> TotalCoinsChanged;
    public event UnityAction GameOver;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
    }

    private void OnEnable()
    {
        _playerData.BestScoreLoaded += OnBestScoreLoaded;
        _playerData.TotalCoinsLoaded += OnTotalCoinsLoaded;
    }

    private void OnDisable()
    {
        _playerData.BestScoreLoaded -= OnBestScoreLoaded;
        _playerData.TotalCoinsLoaded -= OnTotalCoinsLoaded;
    }

    public void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);

        if (_score > _bestScore)
        {
            _bestScore = _score;
            BestScoreChanged?.Invoke(_bestScore);
        }    
    }

    public void IncreaseCoins()
    {
        _coins++;
        CoinsChanged?.Invoke(_coins);

        _totalCoins++;
        TotalCoinsChanged?.Invoke(_totalCoins);
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

    private void OnBestScoreLoaded(int amount)
    {
        _bestScore = amount;
        BestScoreChanged?.Invoke(_bestScore);
    }

    private void OnTotalCoinsLoaded(int amount)
    {
        _totalCoins = amount;
        TotalCoinsChanged?.Invoke(_totalCoins);
    }
}
