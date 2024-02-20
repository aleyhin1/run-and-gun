using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private PooledObject _pooledObject;

    void Update()
    {
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _pooledObject.ReleaseToBulletPool();
    }
}
