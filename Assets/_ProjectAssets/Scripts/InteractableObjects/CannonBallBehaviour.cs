using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBallBehaviour : MonoBehaviour
{

    public static Action onCannonBallShoot;

    [Header("Attributes")]
    public float speed;

    private float _currentSpeed;

    private int _index;
    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _currentSpeed;
    }
    
    public void Shoot( Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        onCannonBallShoot?.Invoke();
        _currentSpeed = speed;
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
