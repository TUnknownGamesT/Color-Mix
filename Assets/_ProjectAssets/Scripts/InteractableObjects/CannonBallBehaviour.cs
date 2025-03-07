using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class CannonBallBehaviour : MonoBehaviour
{

    public static Action onCannonBallShoot;

    [Header("Attributes")]
    public float speed;

    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private ParticleSystem _nucleus;
    [SerializeField]
    private ParticleSystem _trail;



    private float _currentSpeed;

    private int _index;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _currentSpeed;
    }

    public void Shoot(Color color)
    {
        _meshRenderer.material.SetColor("_Color", color);
        var nucleusMain = _nucleus.main;
        nucleusMain.startColor = color;
        var trailMain = _trail.main;
        trailMain.startColor = color;
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
