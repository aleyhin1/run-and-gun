using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action OnMovement;
    public event Action OnIdle;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private PlayerAnimation _playerAnim;
    private float _time = 0;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (movementVector != Vector3.zero)
        {
            Move(movementVector);
            TurnTo(movementVector);
            OnMovement?.Invoke();
        }
        else
        {
            _time = 0;
            OnIdle?.Invoke();
        }
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
