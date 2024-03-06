using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class War : MonoBehaviour
{
    private CardDeck cardPlayer1Deck, cardPlayer2Deck;
    [SerializeField] private Button player1Stack, player2Stack;
    [SerializeField] private TextMeshProUGUI p1Cards, p2Cards;
    [SerializeField] private GameObject deck;
    [SerializeField] private GameObject playAgain;
    private int playerOneCards = 0;
    private int playerTwoCards= 0;

    // FOR TESTING
    public TextMeshProUGUI txt_Winner;

    //Enums for turn base
    enum PlayerTurn
    {
        PLAYER1,
        PLAYER2
    }

    //Keep Track of turn
    private PlayerTurn playerTurn = PlayerTurn.PLAYER1;

    public void PlayCard()
    {
        cardPlayer1Deck = GameManager.instance.cardDeck[0];
        cardPlayer2Deck = GameManager.instance.cardDeck[1];

        CompareCardRanks(cardPlayer1Deck.GetCurrentCard(), cardPlayer2Deck.GetCurrentCard());
        SwitchTurn();
        CheckForWin();
    }

    private int GetCardRank(Sprite cardSprite)
    {
        Debug.Log(cardSprite.name);
        //  GameManager.instance.cardDeck;
        string[] cardName = cardSprite.name.Split('_');
        string rankString = cardName[0]; // Getting the rank based on Val's naming convetion 4BCU = 4 black of clubs JBH = Jack black of Hearts

        int rankValue; // Parse to numeric value

        // If the rank is a number then return the value
        // Else go into the switchase and change it to a value to compare

        if (int.TryParse(rankString, out rankValue)) return rankValue;
        else
        {
            switch (rankString)
            {
                case "A":
                    return 14;
                case "K":
                    return 13;
                case "Q":
                    return 12;
                case "J":
                    return 11;
                default: return 0;
            }
        }
    }

    public bool CheckForWin()
    {
        if (playerOneCards >= 52 || playerTwoCards >= 52) {
            if (playerOneCards >= 52) {
                txt_Winner.SetText("Player One Wins!");
            }else if (playerTwoCards >= 52) {
                txt_Winner.SetText("Player Two Wins!");
            }
            deck.SetActive(false);
            playAgain.SetActive(true);
            return true;
        }
        return false;
    }

    public void CompareCardRanks(Sprite sprite1, Sprite sprite2)
    {
        int player1Rank = GetCardRank(sprite1);
        int player2Rank = GetCardRank(sprite2);

        Debug.Log("Player One Rank: " + player1Rank);
        Debug.Log("Player Two Rank: " + player2Rank);

        if (player1Rank > player2Rank)
        {
            // TODO Add one where it keeps track of the players hand and who has what
            Debug.Log("Player 2 is higher");
            txt_Winner.SetText("Player 1 is higher");
            playerOneCards += 2;
            p1Cards.SetText(playerOneCards + "");
        }
        else if (player1Rank < player2Rank)
        {
            // TEST
            Debug.Log("Player 1 is higher");
            txt_Winner.SetText("Player 2 is higher");
            playerTwoCards += 2;
            p2Cards.SetText(playerTwoCards + "");

        }
        else if (player1Rank == player2Rank)
        {
            Debug.Log("Are equal");
            txt_Winner.SetText("Are Equal");
        }
    }

    public void SwitchTurn()
    {
        if (playerTurn == PlayerTurn.PLAYER1) { playerTurn = PlayerTurn.PLAYER2; player1Stack.enabled = false; player2Stack.enabled = true; }
        else if(playerTurn == PlayerTurn.PLAYER2) { playerTurn = PlayerTurn.PLAYER1; player2Stack.enabled = false; player1Stack.enabled = true; }
    }
}
