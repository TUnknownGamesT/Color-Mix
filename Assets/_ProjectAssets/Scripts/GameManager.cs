using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static Action onLvlCompleted;

    public static Action onNewLvlStarted;

    public LvlSettings[] settings;

    [Header("Refferences")]
    public EnemySpawner enemySpawner;
    private int _lvlSettingsIndex;


    void OnEnable()
    {
        EnemySpawner.onAllEnemiesDied += LevelCommpleted;
    }

    void OnDisable()
    {
        EnemySpawner.onAllEnemiesDied -= LevelCommpleted;
    }

    void Start()
    {
        StartGame();
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        if (_lvlSettingsIndex < settings.Length - 1)
        {
            enemySpawner.StartLvl(settings[_lvlSettingsIndex]);
            _lvlSettingsIndex++;
            onNewLvlStarted?.Invoke();
        }
        else
        {
            Debug.Log("All lvls Completed");
        }
    }

    public void LevelCommpleted()
    {
        onLvlCompleted?.Invoke();
    }


}
