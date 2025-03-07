using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGmaeplayManager : MonoBehaviour
{
    public GameObject colorGrabber;

    [Header("Main Menu")]

    public TextMeshProUGUI mainMenuScoreText;

    public Canvas mainMenuCanvas;

    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        ColorContainerConmtroller.onColorContainerTouched += OnColorContainerTouched;
        EnemyBehaviour.onEnemyDie += UpdateScore;
        GameManager.onLvlCompleted += ShowMainMenu;
    }

    private void OnDisable()
    {
        ColorContainerConmtroller.onColorContainerTouched -= OnColorContainerTouched;
        EnemyBehaviour.onEnemyDie -= UpdateScore;
        GameManager.onLvlCompleted -= ShowMainMenu;
    }

    private void OnColorContainerTouched(Color color)
    {
        colorGrabber.SetActive(true);
        colorGrabber.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        colorGrabber.GetComponent<RawImage>().color = color;
    }

    private void UpdateScore(int amount)
    {
        int score = scoreText.text == "" ? 0 : int.Parse(scoreText.text.Split(':')[1]);
        score += amount;
        scoreText.text = $"Score: {score}";
    }

    public void ShowMainMenu()
    {
        mainMenuScoreText.text = scoreText.text;
        scoreText.text = "";
        ActivateCanvas(mainMenuCanvas);
    }

    public void HideMainMenu()
    {
        scoreText.text = mainMenuScoreText.text;
        mainMenuScoreText.text = "";
        DeactivateCanvas(mainMenuCanvas);
    }

    private void DeactivateCanvas(Canvas canvas)
    {
        canvas.enabled = false;
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    private void ActivateCanvas(Canvas canvas)
    {
        canvas.enabled = true;
        canvas.GetComponent<GraphicRaycaster>().enabled = true;
    }
}
