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
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
                {
                    LevelStarted?.Invoke();
                    _isLevelCanBeStarted = false;
                }
            }
        }
    }
}
