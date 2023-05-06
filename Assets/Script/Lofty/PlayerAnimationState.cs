using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    public static PlayerAnimationState instance;
    private PlayerState _state;
    private Animator _animator;
    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        Falling,
        Attack,
        AirAttack,
        Hurt,
        Die,
    }

    public PlayerState State
    {
        get { return _state; }
        set { _state = value; }
    }

    private void Start()
    {
        instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (_state)
        {
            case PlayerState.Idle:
                SetAnimationFalse();
                _animator.SetBool("Idle",true);
                break;
            case PlayerState.Walk:
                SetAnimationFalse();
                _animator.SetBool("Walk",true);
                break;
            case PlayerState.Attack:
                SetAnimationFalse();
                _animator.SetBool("Attack",true);
                break;
            case PlayerState.AirAttack:
                SetAnimationFalse();
                _animator.SetBool("AirAttack",true);
                break;
            case PlayerState.Jump:
                SetAnimationFalse();
                _animator.SetBool("Jump",true);
                break;
            case PlayerState.Falling:
                SetAnimationFalse();
                _animator.SetBool("Falling",true);
                break;
            case PlayerState.Hurt:
                SetAnimationFalse();
                _animator.SetBool("Hurt",true);
                break;
            case PlayerState.Die:
                SetAnimationFalse();
                _animator.SetBool("Die",true);
                break;

        }
    }

    private void SetAnimationFalse()
    {
        _animator.SetBool("Idle",false);
        _animator.SetBool("Walk",false);
        _animator.SetBool("Jump",false);
        _animator.SetBool("Falling",false);
        _animator.SetBool("Attack",false);
        _animator.SetBool("AirAttack",false);
        _animator.SetBool("Hurt",false);
        _animator.SetBool("Die",false);
    }
}
