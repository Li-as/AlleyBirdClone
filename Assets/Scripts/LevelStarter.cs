using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelStarter : MonoBehaviour
{
    public event UnityAction LevelStarted;

    private bool _isLevelCanBeStarted = true;

    private void Update()
    {
        if (_isLevelCanBeStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    LevelStarted?.Invoke();
                    _isLevelCanBeStarted = false;
                }
            }
        }
    }
}
