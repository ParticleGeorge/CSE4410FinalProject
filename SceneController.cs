using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public int gridRows = 2;
    public int gridColumns = 4;
    public float offsetX = 2.0f;
    public float offsetY = 2.5f;

    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    float cardScale = 4;

    private int score = 0;

    private int strikesLeft = 3;

    [SerializeField] TMP_Text scoreLabel;

    private int failedAttempts = 0;
    [SerializeField] private int maxFails = 3;

    [SerializeField] private GameOverUI gameOverUI;

    [SerializeField] TMP_Text strikesLabel;

    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    private bool allowRevealing = false;
    public bool canReveal { get { return allowRevealing && secondRevealed == null; } }


    public void SetupGame()
    {

        Vector3 startPos = originalCard.transform.position;

        int[] numbers = new int[gridRows * gridColumns];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i / 2;
        }

        numbers = ShuffleArray(numbers);

        float spacingX = 3.0f / gridColumns;
        float spacingY = 3.5f / gridRows;

        float scale = Mathf.Min(spacingX, spacingY);

        for (int i = 0; i < gridColumns; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard);
                }

                int index = j * gridColumns + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (i - (gridColumns - 1) / 2f) * (2.0f * scale);
                float posY = ((gridRows - 1) / 2f - j) * (2.5f * scale);
                card.transform.position = new Vector3(posX, posY, startPos.z);
                card.transform.localScale = Vector3.one * scale;
            }
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            scoreLabel.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(1);
            firstRevealed.HideCardFace();
            secondRevealed.HideCardFace();

            strikesLeft--;
            strikesLabel.text = "Strikes Left: " + strikesLeft;

            if (strikesLeft <= 0)
            {
                GameOver();
            }
        }


        firstRevealed = null;
        secondRevealed = null;
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int random = Random.Range(i, newArray.Length);
            int temp = newArray[i];
            newArray[i] = newArray[random];
            newArray[random] = temp;
        }

        return newArray;
    }

    // Methods to show/hide all cards
    public void RevealAll()
    {
        foreach (MemoryCard card in FindObjectsOfType<MemoryCard>())
        {
            card.ShowCardFace();
        }
    }

    public void HideAll()
    {
        foreach (MemoryCard card in FindObjectsOfType<MemoryCard>())
        {
            card.HideCardFace();
        }
    }

    public void EnableRevealing(bool enable)
    {
        allowRevealing = enable;
    }


    void ResetGameState()
    {
        failedAttempts = 0;
        score = 0;
        scoreLabel.text = "Score: 0";
    }

    public void easySetup()
    {
        hideButtons();
        gridRows = 2;
        gridColumns = 4;
        ResetGameState();
        SetupGame();
        strikesLeft = 3;
        strikesLabel.text = "Strikes Left: " + strikesLeft;

    }

    public void mediumSetup()
    {
        hideButtons();
        gridRows = 4;
        gridColumns = 4;
        ResetGameState();
        SetupGame();
    }

    public void hardSetup()
    {
        hideButtons();
        gridRows = 4;
        gridColumns = 6;
        ResetGameState();
        SetupGame();
    }

    void OnEnable()
    {
        easyButton.onClick.AddListener(() => easySetup());
        mediumButton.onClick.AddListener(() => mediumSetup());
        hardButton.onClick.AddListener(() => hardSetup());
    }

    void hideButtons()
    {
        easyButton.gameObject.SetActive(false);
        mediumButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
    }

    public void ClearCards()
    {
        // Destroy all cards except the original prefab
        foreach (MemoryCard card in FindObjectsOfType<MemoryCard>())
        {
            if (card != originalCard && card.gameObject != null)
            {
                Destroy(card.gameObject);
            }
        }

        // Reset revealed cards
        firstRevealed = null;
        secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void GameOver()
    {
        EnableRevealing(false);
        gameOverUI.Show(score);
    }


}
