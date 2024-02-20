using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerMovement;

    private void Start()
    {
        _playerMovement.OnMovement += SetWalkAnimation;
        _playerMovement.OnIdle += SetIdleAnimation;
    }

    public void SetShootAnimation()
    {
        _animator.SetBool("isShooting", true);
        _animator.SetBool("isMoving", false);
    }

    public void SetWalkAnimation()
    {
        _animator.SetBool("isMoving", true);
        _animator.SetBool("isShooting", false);
    }

    public void SetIdleAnimation()
    {
        _animator.SetBool("isMoving", false);
        _animator.SetBool("isShooting", false);
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        _animator.SetFloat("moveSpeed", moveSpeed);
    }
}
