using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private TouchControls touchControls;

    private Camera mainCamera;

    void Awake()
    {
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        touchControls.Enable();
    }

    void OnDisable()
    {
        touchControls.Disable();
    }

    void Start()
    {
        touchControls.Touch.TouchInput.started += ctx => TouchedScreen(ctx);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.GetComponent<IInteractable>() != null)
                {
                    hit.collider.GetComponent<IInteractable>().OnTouched(Input.mousePosition);
                }
            }
        }
    }


    //TODO switch to new input system
    private void TouchedScreen(InputAction.CallbackContext context)
    {
        Debug.Log("Touched");
        Ray ray = mainCamera.ScreenPointToRay(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                // hit.collider.GetComponent<IInteractable>().OnTouched(context);
            }
        }
    }


}
