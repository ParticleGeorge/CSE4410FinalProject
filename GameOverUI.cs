using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    public Button mainMenuButton;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        gameObject.SetActive(false);
    }

    public void Show(int finalScore)
    {
        scoreText.text = "Score: " + finalScore;
        gameObject.SetActive(true);
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
