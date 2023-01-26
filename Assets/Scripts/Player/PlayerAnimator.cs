using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerController _controller;
    private Animator _animator;

    private Vector3 _prevPosition;

    private void Awake()
    {
        _prevPosition = transform.position;
        _animator = GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 velocity = (transform.position - _prevPosition);
        _prevPosition = transform.position;
        if (velocity.sqrMagnitude > 0)
            velocity = velocity.normalized;
        PlayerController.State state = _controller.CurrentState;
        _animator.SetFloat("x_speed", velocity.x);
        _animator.SetFloat("y_speed", velocity.y);
        _animator.SetFloat("speed", velocity.x * velocity.x + velocity.y * velocity.y);
    }
}
