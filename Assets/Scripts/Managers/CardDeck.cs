using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    // Will need a List for the images
    public List<Sprite> cardsImages = new List<Sprite>();
    private string folderPath = "deck-of-cards/Black Cards";// Path to set cards dynamically

    public int currentCardIndex = 0;

    private void Start()
    {
        LoadCardSprites();
        ShuffleDeck();
    }

    // Display cards at random. Need to set up a number array or list (Might need to specify values) to compare
    // 52 cards, Could check if it contains the specific name title.

    // Load card sprites from folder
    private void LoadCardSprites()
    {
        Sprite[] cardSprites = Resources.LoadAll<Sprite>(folderPath);
        foreach (Sprite sprite in cardSprites)
        {
            // TEST
            //Debug.Log("Sprite Name " + sprite.name);
            cardsImages.Add(sprite);
        }
    }

    // Shuffle Cards
    public void ShuffleDeck()
    {

        System.Random rand = new System.Random();
        int numberOfCards = cardsImages.Count; // Amount of cards

        while (numberOfCards > 1)
        {
            numberOfCards--; //Removing cards
            int randomCard = rand.Next(numberOfCards);
            Sprite valueOfCard = cardsImages[randomCard];
            cardsImages[randomCard] = cardsImages[numberOfCards];
            cardsImages[numberOfCards] = valueOfCard;
            // TEST
            //Debug.Log("Card Name: " + cardsImages[randomCard].name);    

        }

    }

    // 
    public Sprite GetCurrentCard()
    {
        if (cardsImages.Count > 0) return cardsImages[currentCardIndex];
        else return null;

    }

    public void RemoveCurrentCard()
    {
        if (cardsImages.Count > 0) cardsImages.RemoveAt(0);
    }

    public void CurrentImage(Image currentCardImage)
    {
        currentCardIndex++;
        if (currentCardIndex >= cardsImages.Count) currentCardIndex = 0;
        if (currentCardIndex != null) currentCardImage.sprite = GetCurrentCard();
    }


}
