using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private string _appearAnimationTrigger;
    [SerializeField] private string _disappearAnimationTrigger;
    [SerializeField] private Button _restartButton;
    [SerializeField] private LevelRestarter _levelRestarter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        Disappear();
        _levelRestarter.Restart();
    }

    private void Disappear()
    {
        _animator.SetTrigger(_disappearAnimationTrigger);
        gameObject.SetActive(false);
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(_appearAnimationTrigger);
    }
}
