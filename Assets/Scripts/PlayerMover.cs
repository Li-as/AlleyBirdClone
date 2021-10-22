using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxJumps;

    private Rigidbody2D _rigidbody;
    private Vector2 _faceDirection;
    private int _currentJumps;

    public Vector3 StartPosition => _startPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _faceDirection = Vector2.right;
    }

    private void Update()
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

    private void FixedUpdate()
    {
        float velocityX = _speed * _faceDirection.x * Time.fixedDeltaTime;
        float velocityY = _rigidbody.velocity.y;
        _rigidbody.velocity = new Vector2(velocityX, velocityY);
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
    }
}
