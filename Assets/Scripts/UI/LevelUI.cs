using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private ScoreDisplay _scoreDisplay;
    [SerializeField] private CoinsDisplay _coinsDisplay;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _player.GameOver -= OnGameOver;
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        _scoreDisplay.Appear();
        _coinsDisplay.Appear();
    }

    private void OnGameOver()
    {
        _gameOverScreen.Appear();
    }
}
