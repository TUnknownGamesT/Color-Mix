using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Video : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minSpawnTime = 1f; // Minimum time before spawn
    public float maxSpawnTime = 5f; // Maximum time before spawn
    public float spawnDistance = 5f; // Spawn range distance

    void Start()
    {
        UniTask.Void(async () =>
        {
            while (true)
            {
                float waitTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
                await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

                SpawnEnemy();
            }
        });
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = GetRandomPosition();
        var enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        enemy.GetComponent<EnemyBehaviour>().ChangeColor(GetRandomColor());
    }

    private Vector3 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(-spawnDistance, spawnDistance);
        float z = UnityEngine.Random.Range(-spawnDistance, spawnDistance);
        return new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1f);
    }
}