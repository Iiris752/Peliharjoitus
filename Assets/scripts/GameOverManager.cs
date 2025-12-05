using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;   //Canvas

    private void Start()
    {
        // Piilotetaan GameOver alussa
        gameOverCanvas.enabled = false;
    }

    //kutsutaan metodia Haamunaivot tai pointNClick scriptistä, jos pisteet menee nolliin:
    public void ShowGameOver(string message)
    {
        // Näytetään Canvas ja asetetaan teksti
        gameOverCanvas.enabled = true;

        // Pysäytetään peli
        Time.timeScale = 0f;
    }
}
