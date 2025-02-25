using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CannonBallsSpawner : MonoBehaviour
{
    public GameObject cannonBallPrefab;

    public Transform spawnPoint;


    void OnEnable()
    {
        CannonBallBehaviour.onCannonBallShoot += SpawnCannonBall;
    }

    void OnDisable()
    {
        CannonBallBehaviour.onCannonBallShoot -= SpawnCannonBall;
    }

    void Start()
    {

    }

    public void SpawnCannonBall()
    {
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            Instantiate(cannonBallPrefab, spawnPoint.position, spawnPoint.rotation);
            await UniTask.Yield();
        });

    }
}
