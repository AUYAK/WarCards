using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck,
               EnemyHand, PlayerHand,
               EnemyField, PlayerField;
    public Game() {
        EnemyDeck = GiveDeckCart();
        PlayerDeck = GiveDeckCart();

        EnemyHand = new List<Card>();
        EnemyField = new List<Card>();

        PlayerHand = new List<Card>();
        PlayerField = new List<Card>();
    }

    List<Card> GiveDeckCart()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        return list;

    }

}

public class GameManagerScr : MonoBehaviour {
    public Game currentGame;
    public Transform enemyHand, playerHand;
    public GameObject CardPref;
    int turn, turnTime = 30;
    public TextMeshProUGUI turnTimeTxt;
    public Button EndTurnButton;
    public bool IsPlayerTurn
    {
        get { return turn % 2 == 0; }
    }
    void Start()
    {
        turn = 0;
        
        currentGame = new Game();
        GiveHandCards(currentGame.EnemyDeck, enemyHand);
        GiveHandCards(currentGame.PlayerDeck, playerHand);

        StartCoroutine(TurnFunc());
    }
    void GiveHandCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 4) GiveCardToHand(deck,hand);
    }

    void GiveCardToHand(List<Card> deck,Transform hand)
    {
        if (deck.Count == 0) return;

        Card card = deck[0];

        GameObject cardGO = Instantiate(CardPref, hand, false);
        if (hand == enemyHand)
            cardGO.GetComponent<CardInfoScr>().HideCardInfo(card);
        else cardGO.GetComponent<CardInfoScr>().ShowCardInfo(card);
        deck.RemoveAt(0);
    }

    public void ChangeTurn()
    {
        StopAllCoroutines() ;
        turn++;

        EndTurnButton.interactable = IsPlayerTurn;
        if (IsPlayerTurn)
        {
            GiveNewCards();
        }
        StartCoroutine(TurnFunc());
    }

    private void GiveNewCards()
    {
        GiveCardToHand(currentGame.EnemyDeck, enemyHand);
        GiveCardToHand(currentGame.PlayerDeck, playerHand);
    }
    IEnumerator TurnFunc()
    {
        turnTime = 30;
        turnTimeTxt.SetText(turnTime.ToString()) ;

        if (IsPlayerTurn)
        {
            while (turnTime-- > 0)
            {
                turnTimeTxt.text = turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (turnTime-- > 27)
            {
                turnTimeTxt.text = turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        ChangeTurn();
    }
}
