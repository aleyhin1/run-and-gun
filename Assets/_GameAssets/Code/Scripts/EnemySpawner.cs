using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnCooldown;
    private Dictionary<SpawnPoint, bool> _spawnPoints = new Dictionary<SpawnPoint, bool>();
    private float _time;

    private void Start()
    {
        FillSpawnPoints();
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time > _spawnCooldown)
        {
            Debug.Log(GetRandomSpawnablePoint().transform.position);
            _time = 0;
        }
    }

    private SpawnPoint GetRandomSpawnablePoint()
    {
        var spawnablePoints = GetSpawnablePoints();

        if (spawnablePoints.Count() < 1) { return null; }

        int randomIndex = Random.Range(0, spawnablePoints.Count());
        return spawnablePoints.ToList()[randomIndex].Key;
    }

    private IEnumerable<KeyValuePair<SpawnPoint, bool>> GetSpawnablePoints()
    {
        return _spawnPoints.Where(x => x.Value == true);
    }

    private void FillSpawnPoints()
    {
        foreach (SpawnPoint spawnPoint in GetComponentsInChildren<SpawnPoint>())
        {
            spawnPoint.Spawner = this;
            _spawnPoints.Add(spawnPoint, false);
        }
    }

    public void SetPointAvailable(SpawnPoint point, bool state)
    {
        _spawnPoints[point] = state;
    }

}
