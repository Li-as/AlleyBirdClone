using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class StartUI : MonoBehaviour
{
    [SerializeField] private string _appearAnimationTrigger;
    [SerializeField] private string _disappearAnimationTrigger;
    [SerializeField] private LevelStarter _levelStarter;
    [SerializeField] private float _disappearDuration;
    [SerializeField] private LevelUI _levelUI;

    private Animator _animator;

    public event UnityAction StartUIDisappeard;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _levelStarter.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _levelStarter.LevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        _animator.SetTrigger(_disappearAnimationTrigger);
        StartCoroutine(WaitForEndOfDisappear(_disappearDuration));
    }

    private IEnumerator WaitForEndOfDisappear(float duration)
    {
        yield return new WaitForSeconds(duration);
        _levelUI.Appear();
        StartUIDisappeard?.Invoke();
    }
}
