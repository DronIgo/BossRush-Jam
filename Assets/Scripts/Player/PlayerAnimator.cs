using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController)), RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private PlayerController _controller;
    private Animator _animator;
    private Animator _crossAnimator;
    private GameObject _chargedIndicator;
    private Vector3 _prevPosition;

    private void Start()
    {
        _prevPosition = transform.position;
        _animator = GetComponent<Animator>();
        _crossAnimator = transform.Find("Cross").GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
        SetAnimationsSpeed();
        _chargedIndicator = transform.Find("Indicator").gameObject;
    }

    private float dashAnimatorSpeed = 1f;
    private float moveAnimatorSpeed = 1f;
    private float attackAnimatorSpeed = 1f;
    private float defaultAnimatorSpeed = 1f;

    Vector2 _lastNotZeroDirection = new Vector2(0, 0);

    private void Update()
    {
        Vector2 velocity = _controller.Direction;
        if (velocity.x != 0 || velocity.y != 0)
            _lastNotZeroDirection = velocity;
        _prevPosition = transform.position;
        if (velocity.sqrMagnitude > 0)
            velocity = velocity.normalized;
        PlayerController.State moveState = _controller.MovementState;
        PlayerController.State crossState = _controller.CrossState;
        _animator.SetFloat("x_speed", velocity.x);
        _animator.SetFloat("y_speed", velocity.y);
        _crossAnimator.SetFloat("x_speed", velocity.x);
        _crossAnimator.SetFloat("y_speed", velocity.y);
        _animator.SetFloat("speed", velocity.x * velocity.x + velocity.y * velocity.y);
        _animator.SetFloat("x_direction", _lastNotZeroDirection.x);
        _animator.SetFloat("y_direction", _lastNotZeroDirection.y);
        switch (moveState)
        {
            case PlayerController.State.Move:
                _animator.SetBool("dash", false);
                _animator.speed = moveAnimatorSpeed;
                break;
            case PlayerController.State.Roll:
                _animator.SetBool("dash", true);
                _animator.speed = dashAnimatorSpeed;
                break;
        }

        switch(crossState)
        {
            case PlayerController.State.Move:
                _crossAnimator.SetBool("attack", false);
                _crossAnimator.SetBool("charged_attack", false);
                _crossAnimator.speed = defaultAnimatorSpeed;
                break;
            case PlayerController.State.Attack:
                _crossAnimator.SetBool("attack", true);
                _crossAnimator.SetBool("charged_attack", false);
                _crossAnimator.speed = attackAnimatorSpeed;
                break;
            case PlayerController.State.ChargedAttack:
                _crossAnimator.SetBool("attack", false);
                _crossAnimator.SetBool("charged_attack", true);
                _crossAnimator.speed = attackAnimatorSpeed;
                break;
        }

        _chargedIndicator.SetActive(_controller.charged);
    }


    private void SetAnimationsSpeed()
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Roll")
            {
                float duration = clip.length;
                dashAnimatorSpeed = duration/PlayerController.Instance.dashDuration;
            }
        }
        clips = _crossAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack")
            {
                float duration = clip.length;
                attackAnimatorSpeed = duration / PlayerController.Instance.attackDuration - 0.08f;
            }
        }
    }
}
