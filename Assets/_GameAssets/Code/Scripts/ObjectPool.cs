using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance {  get; private set; }
    [SerializeField] private uint _bulletPoolSize;
    [SerializeField] private PooledObject _bulletToPool;
    private Stack<PooledObject> _bulletStack;

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

    public void ReturnToBulletPool(PooledObject pooledObject)
    {
        _bulletStack.Push(pooledObject);
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
}
