using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _best;
    [SerializeField] private string _bestStartString;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
        _player.BestScoreChanged += OnBestScoreChanged;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
        _player.BestScoreChanged -= OnBestScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }    
    
    private void OnBestScoreChanged(int score)
    {
        _best.text = _bestStartString + score.ToString();
    }

    public void Appear()
    {
        _canvasGroup.alpha = 1;
    }
}
