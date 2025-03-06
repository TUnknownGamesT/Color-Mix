using UnityEngine;

[CreateAssetMenu(fileName = "LvlSettings", menuName = "Scriptable Objects/LvlSettings")]
public class LvlSettings : ScriptableObject
{
    public float spawnTime;
    public int enemiesNumber;
}
