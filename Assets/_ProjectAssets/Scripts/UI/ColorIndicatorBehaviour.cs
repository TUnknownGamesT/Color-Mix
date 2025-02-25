using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorIndicatorBehaviour : MonoBehaviour, IDropHandler, IDragHandler
{

    private RawImage colorImage;

    private RectTransform rectTransform;

    void Awake()
    {
        colorImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("CannonBall"))
            {
                hit.collider.GetComponent<CannonBallBehaviour>().ChangeColor(colorImage.color);
                gameObject.SetActive(false);
            }
        }
    }
}
