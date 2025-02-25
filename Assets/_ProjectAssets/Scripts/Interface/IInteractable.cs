using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void OnTouched(Vector2 position);
}
