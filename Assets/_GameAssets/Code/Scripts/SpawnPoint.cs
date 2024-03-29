using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public EnemySpawner Spawner { get; set; }

    private void OnBecameInvisible()
    {
        Spawner.SetPointAvailable(this, true);
    }

    private void OnBecameVisible()
    {
        Spawner.SetPointAvailable(this, false);
    }
}
