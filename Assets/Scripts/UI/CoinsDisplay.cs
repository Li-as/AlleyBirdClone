using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private TMP_Text _total;
    [SerializeField] private string _totalStartString;

    private void OnEnable()
    {
        _player.CoinsChanged += OnCoinsChanged;
    }

    private void OnDisable()
    {
        _player.CoinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        _coins.text = coins.ToString();
        _total.text = _totalStartString + coins.ToString();
    }
}
