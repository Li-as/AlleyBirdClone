using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelStarter : MonoBehaviour
{
    public event UnityAction LevelStarted;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                LevelStarted?.Invoke();
            }
        }
    }
}
