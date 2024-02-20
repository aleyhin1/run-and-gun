using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement.OnMovement += SetWalkAnimation;
        _playerMovement.OnIdle += SetIdleAnimation;
    }

    public void SetWalkAnimation()
    {
        _animator.SetBool("isMoving", true);
    }

    public void SetIdleAnimation()
    {
        _animator.SetBool("isMoving", false);
    }
}
