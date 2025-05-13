using System.Collections;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] SceneController controller;

    private int _id;
    public int Id => _id;

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (IsFaceDown() && controller.canReveal)
        {
            ShowCardFace();
            controller.CardRevealed(this);
        }
    }

    public void ShowCardFace()
    {
        cardBack.SetActive(false);
    }

    public void HideCardFace()
    {
        cardBack.SetActive(true);
    }

    public bool IsFaceDown()
    {
        return cardBack.activeSelf;
    }
}
