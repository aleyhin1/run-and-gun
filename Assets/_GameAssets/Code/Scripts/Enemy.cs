using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _playerTransform;

    private void OnEnable()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 directionToMove = (_playerTransform.position - transform.position).normalized;
        transform.Translate(directionToMove * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Die();
            //collision.collider.GetComponent<PooledObject>().ReleaseToBulletPool();
        }
    }

    private void Die()
    {
        GetComponent<PooledObject>().ReleaseToEnemyPool();
    }
}
