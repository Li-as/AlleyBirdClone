using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private ScoreDisplay _scoreDisplay;
    [SerializeField] private CoinsDisplay _coinsDisplay;

    public void Appear()
    {
        gameObject.SetActive(true);
        _scoreDisplay.gameObject.SetActive(true);
        _coinsDisplay.gameObject.SetActive(true);
    }
}
