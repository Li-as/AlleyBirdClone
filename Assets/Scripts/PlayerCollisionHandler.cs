using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerMover))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;
    private PlayerMover _mover;
    private Vector2 _lastCollidedPlatformPosition;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _mover = GetComponent<PlayerMover>();
        _lastCollidedPlatformPosition = _mover.StartPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerFlipper flipper))
        {
            _mover.Flip();
        }
        else if (collision.TryGetComponent(out Coin coin))
        {
            _player.IncreaseCoins();
            coin.gameObject.SetActive(false);
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            _player.Die();
        }
        else if (collision.transform.TryGetComponent(out Platform platform))
        {
            if (collision.contacts[0].normal.y > 0)
            {
                if (platform.transform.position.y > _lastCollidedPlatformPosition.y)
                {
                    _player.IncreaseScore();
                    _lastCollidedPlatformPosition = platform.transform.position;
                }    
            }
        }
    }

    public void ResetCollision()
    {
        _lastCollidedPlatformPosition = _mover.StartPosition;
    }
}
