using System;
using UnityEngine;

public class CannonBallChecker : MonoBehaviour
{
    public static Action onCannonBallMoved;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CannonBall"))
            onCannonBallMoved?.Invoke();
    }

}
