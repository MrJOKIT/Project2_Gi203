using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Open",true);
    }

    public void OpenDoor()
    {
        _animator.SetBool("Open",true);
    }

    public void CloseDoor()
    {
        _animator.SetBool("Open",false);
    }
}
