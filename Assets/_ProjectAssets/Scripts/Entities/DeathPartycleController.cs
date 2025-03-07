using UnityEngine;

//TODO find a better way to handle this
public class DeathPartycleController : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.onNewLvlStarted += DestroyParticle;
    }

    void OnDisable()
    {
        GameManager.onNewLvlStarted -= DestroyParticle;
    }

    private void DestroyParticle()
    {
        Destroy(gameObject);
    }
}
