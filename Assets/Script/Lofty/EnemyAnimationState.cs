using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationState : MonoBehaviour
{
    public static EnemyAnimationState instance;
    public EnemyState _state;
    private Animator _animator;
    
    public enum EnemyState
    {
        Idle,
        Walk,
        Alert,
        Attack,
    }

    public EnemyState State
    {
        get { return _state; }
        set { _state = value; }
    }

    private void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                SetAnimationFalse();
                _animator.SetBool("Idle",true);
                break;
            case EnemyState.Walk:
                SetAnimationFalse();
                _animator.SetBool("Walk",true);
                break;
            case EnemyState.Attack:
                SetAnimationFalse();
                _animator.SetBool("Attack",true);
                break;
            case EnemyState.Alert:
                SetAnimationFalse();
                _animator.SetBool("Alert",true);
                break;
        }
    }

    private void SetAnimationFalse()
    {
        _animator.SetBool("Idle",false);
        _animator.SetBool("Walk",false);
        _animator.SetBool("Attack",false);
        _animator.SetBool("Alert",false);
    }
}
