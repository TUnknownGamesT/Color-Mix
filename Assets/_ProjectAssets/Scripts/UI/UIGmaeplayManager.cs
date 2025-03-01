using UnityEngine;
using UnityEngine.UI;

public class UIGmaeplayManager : MonoBehaviour
{
    public GameObject colorGrabber;

    private void OnEnable()
    {
        ColorContainerConmtroller.onColorContainerTouched += OnColorContainerTouched;
    }

    private void OnDisable()
    {
        ColorContainerConmtroller.onColorContainerTouched -= OnColorContainerTouched;
    }

    private void OnColorContainerTouched(Color color)
    {
        colorGrabber.SetActive(true);
        colorGrabber.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        colorGrabber.GetComponent<RawImage>().color = color;
    }
}
