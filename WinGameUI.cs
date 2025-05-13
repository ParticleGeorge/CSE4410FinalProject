using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class WinGameUI : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    
    void Start() {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }
    public void Win()
    {
        gameObject.SetActive(true);
        winText.text = "YOU WIN!!!";
        scoreText.enabled = false;
    }

    void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}