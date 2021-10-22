using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private string _totalCoinsSaveName;
    [SerializeField] private string _bestScoreSaveName;
    [SerializeField] private Player _player;

    public event UnityAction<int> TotalCoinsLoaded;
    public event UnityAction<int> BestScoreLoaded;

    private void OnEnable()
    {
        _player.BestScoreChanged += OnBestScoreChanged;
        _player.TotalCoinsChanged += OnTotalCoinsChanged;
    }

    private void OnDisable()
    {
        _player.BestScoreChanged -= OnBestScoreChanged;
        _player.TotalCoinsChanged -= OnTotalCoinsChanged;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(_totalCoinsSaveName))
        {
            int playerCoins = PlayerPrefs.GetInt(_totalCoinsSaveName);
            TotalCoinsLoaded?.Invoke(playerCoins);
        }        
        
        if (PlayerPrefs.HasKey(_bestScoreSaveName))
        {
            int playerScore = PlayerPrefs.GetInt(_bestScoreSaveName);
            BestScoreLoaded?.Invoke(playerScore);
        }
    }

    private void OnTotalCoinsChanged(int amount)
    {
        PlayerPrefs.SetInt(_totalCoinsSaveName, amount);
    }

    private void OnBestScoreChanged(int amount)
    {
        PlayerPrefs.SetInt(_bestScoreSaveName, amount);
    }
}
