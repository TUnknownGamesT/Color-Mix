using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBallBehaviour : MonoBehaviour
{

    public static Action onCannonBallShoot;

    [Header("Refferences")]
    public List<RawImage> colorImages;

    public Canvas canvas;

    [Header("Attributes")]
    public float speed;


    //private

    private bool _shooted;

    private float _curretnSpeed;

    private int _index;
    private Rigidbody _rb;

    private MeshRenderer _meshRenderer;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _curretnSpeed;
    }

    public void ChangeColor(Color color)
    {
        colorImages[_index].color = color;
        _index++;
        if (_index == colorImages.Count)
        {
            Color mockColor = new Color((colorImages[0].color.r + colorImages[1].color.r) / 2
            , (colorImages[0].color.g + colorImages[1].color.g) / 2
            , (colorImages[0].color.b + colorImages[1].color.b) / 2, 1);

            _meshRenderer.material.color = mockColor;
            canvas.enabled = false;
            _shooted = true;
            Shoot();
        }

    }


    public void Shoot()
    {
        onCannonBallShoot?.Invoke();
        _rb.useGravity = true;
        _curretnSpeed = speed;
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Enemy"))
        {
            if (_shooted == false)
                onCannonBallShoot?.Invoke();
            Destroy(gameObject);
        }
    }
}
