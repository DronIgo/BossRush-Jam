using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController)), RequireComponent(typeof(Animator))]
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
        // На данный момент чтобы определить направление движение не используется инпут вместо этого мы смотрим на разницу положений на предыдущем фрейме и текущем
        Vector2 velocity = _controller.Direction;
        _prevPosition = transform.position;
        if (velocity.sqrMagnitude > 0)
            velocity = velocity.normalized;
        PlayerController.State state = _controller.CurrentState;
        _animator.SetFloat("x_speed", velocity.x);
        _animator.SetFloat("y_speed", velocity.y);
        _animator.SetFloat("speed", velocity.x * velocity.x + velocity.y * velocity.y);
        switch (state)
        {
            case PlayerController.State.Move:
                _animator.SetBool("dash", false);
                _animator.SetBool("attack", false);
                _animator.SetBool("charged_attack", false);
                break;
            case PlayerController.State.Roll:
                _animator.SetBool("dash", true);
                _animator.SetBool("attack", false);
                _animator.SetBool("charged_attack", false);
                break;
            case PlayerController.State.Attack:
                _animator.SetBool("dash", false);
                _animator.SetBool("attack", true);
                _animator.SetBool("charged_attack", false);
                break;
            case PlayerController.State.ChargedAttack:
                _animator.SetBool("dash", false);
                _animator.SetBool("attack", false);
                _animator.SetBool("charged_attack", true);
                break;
        }
    }
}
