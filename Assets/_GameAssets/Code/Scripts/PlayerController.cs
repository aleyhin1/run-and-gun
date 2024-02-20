using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnMovement;
    public event Action OnIdle;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private PlayerAnimation _playerAnim;
    private bool _isShooting;
    private float _shootingCooldown = 1f;
    private float _time = 0;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isShootPressed = Input.GetKeyUp(KeyCode.Space);

        Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (isShootPressed && !_isShooting)
        {
            StartCoroutine(ShootingTimer(_shootingCooldown));
            _playerAnim.SetShootAnimation();
        }
        else if (movementVector != Vector3.zero && !_isShooting)
        {
            Move(movementVector);
            TurnTo(movementVector);
            OnMovement?.Invoke();
        }
        else if (!_isShooting)
        {
            _time = 0;
            OnIdle?.Invoke();
        }
    }

    private IEnumerator ShootingTimer(float cooldown)
    {
        _isShooting = true;
        yield return new WaitForSeconds(cooldown);
        _isShooting = false;
    }

    private void TurnTo(Vector3 directionToTurn)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionToTurn), .1f);
    }

    private void Move(Vector3 direction)
    {
        _time += Time.deltaTime;
        float curveMultiplier = _movementCurve.Evaluate(_time);
        _playerAnim.SetMoveSpeed(curveMultiplier);
        _characterController.Move(direction * _speed * curveMultiplier * Time.deltaTime);
    }
}
