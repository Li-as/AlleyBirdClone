using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipper : MonoBehaviour
{
    [SerializeField] private ScreenSide _screenSide;
    [SerializeField] private float _fromEdgeOffsetX;

    private void Start()
    {
        if (_screenSide == ScreenSide.Left)
        {
            float targetPositionX = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
            targetPositionX -= _fromEdgeOffsetX;
            transform.position = new Vector2(targetPositionX, transform.position.y);
        }
        else
        {
            float targetPositionX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            targetPositionX += _fromEdgeOffsetX;
            transform.position = new Vector2(targetPositionX, transform.position.y);
        }
    }

    private enum ScreenSide
    {
        Left,
        Right
    }
}
