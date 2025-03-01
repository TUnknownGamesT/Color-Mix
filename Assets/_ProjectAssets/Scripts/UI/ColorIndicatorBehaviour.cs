using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorIndicatorBehaviour : MonoBehaviour
{
    private RawImage _colorImage;
    private bool _isDragging;


    void Awake()
    {
        _colorImage = GetComponent<RawImage>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            OnDrop();
        }

        if (_isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Set this to the distance from the camera to the object
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }

    }

    public void OnDrop()
    {
        Debug.Log("OnDrop");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Cannon"))
            {
                hit.collider.GetComponent<CannonBhaviour>().AddColor(_colorImage.color);
            }
        }

        Color c = _colorImage.color;
        c.a = 0f;
        _colorImage.color = c;
    }


}
