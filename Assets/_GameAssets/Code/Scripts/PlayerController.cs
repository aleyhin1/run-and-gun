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
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private ParticleSystem _shootVFX;
    [SerializeField] private AudioSource _audioSource;
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
            StartCoroutine(Shoot());
            _playerAnim.SetShootAnimation();
            _shootVFX.Play();
            _audioSource.PlayOneShot(_audioSource.clip);
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

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(.5f);
        PooledObject bullet = ObjectPool.Instance.GetPooledBullet();
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.rotation = transform.rotation;
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
