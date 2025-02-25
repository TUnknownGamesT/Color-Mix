using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorContainerConmtroller : MonoBehaviour, IInteractable
{
    public static Action<Vector2, Color> onColorContainerTouched;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnTouched(Vector2 position)
    {
        onColorContainerTouched?.Invoke(position, meshRenderer.material.color);
    }
}
