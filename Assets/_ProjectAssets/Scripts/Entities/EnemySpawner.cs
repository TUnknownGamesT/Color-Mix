using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static Action onAllEnemiesDied;

    public List<Material> colors;
    public GameObject enemyPrefab;

    private int _enemiesNumber;

    private int _spawnedEnemies;

    private float _tolerance = 0.01f;

    private float _spawnTime;

    private int _enemyAlive;

    private bool _finishToSpawn;

    private CancellationTokenSource _cts;


    void OnEnable()
    {
        EnemyBehaviour.onEnemyDie += EnemyDied;
    }

    void OnDisable()
    {
        EnemyBehaviour.onEnemyDie -= EnemyDied;
    }

    [ContextMenu("Next Level")]
    public void StartLvl(LvlSettings settings)
    {

        _cts = new CancellationTokenSource();
        _spawnTime = settings.spawnTime;
        _enemiesNumber = settings.enemiesNumber;
        _spawnedEnemies = 0;
        Spawn();
    }

    private void Spawn()
    {
        _finishToSpawn = false;
        _enemyAlive = 0;
        UniTask.Void(async token =>
               {
                   while (_spawnedEnemies < _enemiesNumber)
                   {
                       _spawnedEnemies++;
                       _enemyAlive++;
                       var enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                       RandomizeColor(enemy.GetComponent<EnemyBehaviour>());
                       await UniTask.Delay(TimeSpan.FromSeconds(_spawnTime));
                   }

                   _finishToSpawn = true;

               }, _cts.Token);
    }




    private void RandomizeColor(EnemyBehaviour enemy)
    {
        Color color = colors[UnityEngine.Random.Range(0, colors.Count)].color;
        Color color1 = colors[UnityEngine.Random.Range(0, colors.Count)].color;

        while (AreColorsSimilar(color, color1))
        {
            color1 = colors[UnityEngine.Random.Range(0, colors.Count)].color;
        }

        enemy.ChangeColor(new Color((color.r + color1.r) / 2, (color.g + color1.g) / 2, (color.b + color1.b) / 2, 1));
    }


    bool AreColorsSimilar(Color c1, Color c2)
    {
        return Vector4.Distance(new Vector4(c1.r, c1.g, c1.b, c1.a), new Vector4(c2.r, c2.g, c2.b, c2.a)) <= _tolerance;
    }

    private void EnemyDied(int amount)
    {
        _enemyAlive--;
        if (_enemyAlive == 0 && _finishToSpawn)
        {
            onAllEnemiesDied?.Invoke();
        }
    }


}
