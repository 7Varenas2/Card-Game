using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SlapJack : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1Score;
    [SerializeField] private TextMeshProUGUI player2Score;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Sprite defaultCard;
    [SerializeField] private AudioSource SFX, FlipSFX;

    Sprite topCard;
    Sprite bottomCard;
    int points;
    bool slappable;
    int playerOne;
    int playerTwo;
    [SerializeField] private GameObject slapButton;
    List<Sprite> pile = new List<Sprite>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SFX.Play();
            Slapped(ref playerOne);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            SFX.Play();
            Slapped(ref playerTwo);
        }
        if (Input.GetMouseButtonDown(0))
        {
            FlipSFX.Play();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.name.Equals("slapjack"))
            {
                Flip();
            }
            
        }
    }

    public void Flip()
    {
        
        bottomCard = gameManager.cardDeck[0].GetCurrentCard();
        gameManager.cardDeck[0].ShuffleDeck();
        topCard = gameManager.cardDeck[0].GetCurrentCard();
        gameObject.GetComponent<SpriteRenderer>().sprite = topCard;
        pile.Add(topCard);
        points += 1;

        CheckSlappable();
    }

    public void CheckSlappable()
    {
        if (bottomCard.name.ToCharArray()[0].Equals(topCard.name.ToCharArray()[0]) || topCard.name.ToCharArray()[0].Equals('J'))
        {
            slappable = true;
            slapButton.SetActive(true);

        }
        else
        {
            slappable= false;
            slapButton.SetActive(false);
        }
    }

    public void Slapped(ref int playerScore)
    {

        if (slappable)
        {
            playerScore += points;
            points = 0;
            
            slappable = false;
            foreach (Sprite card in pile)
            {
                gameManager.cardDeck[0].cardsImages.Remove(card);
            }
            CheckWin();
        }



        gameObject.GetComponent<SpriteRenderer>().sprite = defaultCard;
        slapButton.SetActive(false);
    }

    private void CheckWin()
    {
        player1Score.text = "Player 1 Score: " + playerOne.ToString();
        player2Score.text = "Player 2 Score: " + playerTwo.ToString();

        if (gameManager.cardDeck[0].cardsImages.Count <= 0 || !CheckMatches() || playerOne >= 26 || playerTwo >= 26)
        {
            if(playerOne > playerTwo)
            {
                player1Score.text = "Player 1 Winner!!!";
            }
            else
            {
                player2Score.text = "Player 2 Winner!!!";
            }
            GameObject.Destroy(gameObject);
        }

        
    }

    private bool CheckMatches()
    {
        foreach(Sprite card in gameManager.cardDeck[0].cardsImages)
        {
            int matches = 0;
            for(int i = 0; i < gameManager.cardDeck[0].cardsImages.Count; i++)
            {
                if (gameManager.cardDeck[0].cardsImages[i].name.ToCharArray()[0].Equals(card.name.ToCharArray()[0]))
                    matches++;
            }

            if (matches == 0)
                return false;
        }
        return true;
    }
}
