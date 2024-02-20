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

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (movementVector != Vector3.zero)
        {
            OnMovement?.Invoke();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementVector), .1f);
            _characterController.Move(movementVector * Time.deltaTime * _speed);
        }
        else
        {
            OnIdle?.Invoke();
        }
    }
}
