using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool { get; set; }

    public void ReleaseToBulletPool()
    {
        Pool.ReturnToBulletPool(this);
    }
}
