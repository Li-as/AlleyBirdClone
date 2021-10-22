using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private TMP_Text _total;
    [SerializeField] private string _totalStartString;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _player.CoinsChanged += OnCoinsChanged;
        _player.TotalCoinsChanged += OnTotalCoinsChanged;
    }

    private void OnDisable()
    {
        _player.CoinsChanged -= OnCoinsChanged;
        _player.TotalCoinsChanged -= OnTotalCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        _coins.text = coins.ToString();
    }    
    
    private void OnTotalCoinsChanged(int coins)
    {
        _total.text = _totalStartString + coins.ToString();
    }

    public void Appear()
    {
        _canvasGroup.alpha = 1;
    }
}
