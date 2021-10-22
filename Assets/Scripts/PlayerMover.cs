using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxJumps;
    [SerializeField] private StartUI _startUI;

    private Rigidbody2D _rigidbody;
    private Vector2 _faceDirection;
    private int _currentJumps;
    private bool _isCanMove;
    private Player _player;

    public Vector3 StartPosition => _startPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _faceDirection = Vector2.right;
    }

    private void OnEnable()
    {
        _startUI.StartUIDisappeard += OnStartUIDisappeard;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startUI.StartUIDisappeard -= OnStartUIDisappeard;
        _player.GameOver -= OnGameOver;
    }

    private void Update()
    {
        if (_isCanMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentJumps >= _maxJumps)
                {
                    return;
                }

                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _currentJumps++;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isCanMove)
        {
            float velocityX = _speed * _faceDirection.x * Time.fixedDeltaTime;
            float velocityY = _rigidbody.velocity.y;
            _rigidbody.velocity = new Vector2(velocityX, velocityY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Platform platform))
        {
            if (collision.contacts[0].normal.y > 0)
            {
                _currentJumps = 0;
            }
        }
    }

    public void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        _faceDirection *= -1;
    }

    public void ResetState()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector2.zero;
        _faceDirection = Vector2.right;
        _isCanMove = true;
    }

    private void OnStartUIDisappeard()
    {
        _isCanMove = true;
    }

    private void OnGameOver()
    {
        _rigidbody.velocity = Vector2.zero;
        _isCanMove = false;
    }
}
