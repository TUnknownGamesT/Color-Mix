using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBhaviour : MonoBehaviour
{
    [Header("Refferences")]
    public List<RawImage> colorImages;
    public GameObject cannonBallPrefab;
    public Transform shootPoint;
    private int _index;

    public void AddColor(Color color) 
    {
        colorImages[_index].color = color;
        _index++;
        if (_index == colorImages.Count)
        {
            Color mockColor = new Color((colorImages[0].color.r + colorImages[1].color.r) / 2
                , (colorImages[0].color.g + colorImages[1].color.g) / 2
                , (colorImages[0].color.b + colorImages[1].color.b) / 2, 1);
            
            Shoot(mockColor);
        }

    }
    
    private void ClearColors()
    {
        foreach (var image in colorImages)
        {
            image.color = Color.white;
        }
        _index = 0;
    }
    
    private void Shoot(Color color)
    {
        Debug.Log("shoot");
        GameObject ball = Instantiate(cannonBallPrefab, shootPoint.position, shootPoint.rotation);
        ball.GetComponent<CannonBallBehaviour>().Shoot(color);
        ClearColors();
    }

    public void Shoot()
    {
        
    }
}
