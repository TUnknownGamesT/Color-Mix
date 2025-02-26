using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Material> colors;
    public GameObject enemyPrefab;

    public float spawnTime;

    private float tolerance = 0.01f;

    void Start()
    {
        UniTask.Void(async () =>
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(spawnTime));
                var enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                RandomizeColor(enemy.GetComponent<MeshRenderer>());
            }
        });

    }


    private void RandomizeColor(MeshRenderer meshRenderer)
    {
        Color color = colors[UnityEngine.Random.Range(0, colors.Count)].color;
        Color color1 = colors[UnityEngine.Random.Range(0, colors.Count)].color;

        while (AreColorsSimilar(color, color1))
        {
            color1 = colors[UnityEngine.Random.Range(0, colors.Count)].color;
        }

        meshRenderer.material.color = new Color((color.r + color1.r) / 2, (color.g + color1.g) / 2, (color.b + color1.b) / 2, 1);


    }


    bool AreColorsSimilar(Color c1, Color c2)
    {
        //Debug.Log(Vector4.Distance(new Vector4(c1.r, c1.g, c1.b, c1.a), new Vector4(c2.r, c2.g, c2.b, c2.a)));
        return Vector4.Distance(new Vector4(c1.r, c1.g, c1.b, c1.a), new Vector4(c2.r, c2.g, c2.b, c2.a)) <= tolerance;
    }


}
