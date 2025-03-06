using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Material> colors;
    public GameObject enemyPrefab;

    public LvlSettings[] settings;

    private int _enemiesNumber;

    private int _spawnedEnemies;

    private int _lvlSettingsIndex;

    private float _tolerance = 0.01f;

    private float _spawnTime;

    private CancellationTokenSource _cts;


    [ContextMenu("Next Level")]
    public void NextLevel()
    {
        if (_lvlSettingsIndex < settings.Length)
        {
            _cts = new CancellationTokenSource();
            _spawnTime = settings[_lvlSettingsIndex].spawnTime;
            _enemiesNumber = settings[_lvlSettingsIndex].enemiesNumber;
            _lvlSettingsIndex++;
            _spawnedEnemies = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        UniTask.Void(async token =>
               {
                   while (_spawnedEnemies < _enemiesNumber)
                   {
                       _spawnedEnemies++;
                       var enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                       RandomizeColor(enemy.GetComponent<EnemyBehaviour>());
                       await UniTask.Delay(TimeSpan.FromSeconds(_spawnTime));
                   }
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


}
