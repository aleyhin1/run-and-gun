using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance {  get; private set; }
    [SerializeField] private uint _bulletPoolSize;
    [SerializeField] private int _enemyPoolSize;
    [SerializeField] private PooledObject _bulletToPool;
    [SerializeField] private PooledObject _enemyToPool;
    private Stack<PooledObject> _bulletStack;
    private Stack<PooledObject> _enemyStack;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {
        SetupBulletPool();
        SetupEnemyPool();
    }

    public PooledObject GetPooledBullet()
    {
        if (_bulletStack.Count == 0)
        {
            PooledObject newInstance = Instantiate(_bulletToPool, transform);
            newInstance.Pool = this;
            return newInstance;
        }

        PooledObject nextInstance = _bulletStack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public PooledObject GetPooledEnemy()
    {
        if (_enemyStack.Count == 0)
        {
            PooledObject newInstance = Instantiate(_enemyToPool, transform);
            newInstance.Pool = this;
            return newInstance;
        }

        PooledObject nextInstance = _enemyStack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToBulletPool(PooledObject pooledObject)
    {
        _bulletStack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }

    public void ReturnToEnemyPool(PooledObject pooledObject)
    {
        _enemyStack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }

    private void SetupBulletPool()
    {
        _bulletStack = new Stack<PooledObject>();
        PooledObject instance = null;

        for (int i = 0; i < _bulletPoolSize; i++)
        {
            instance = Instantiate(_bulletToPool, transform);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            _bulletStack.Push(instance);
        }
    }

    private void SetupEnemyPool()
    {
        _enemyStack = new Stack<PooledObject>();
        PooledObject instance = null;

        for (int i = 0; i < _enemyPoolSize; i++)
        {
            instance = Instantiate(_enemyToPool, transform);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            _enemyStack.Push(instance);
        }
    }
}
