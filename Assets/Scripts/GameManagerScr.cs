using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;

    public Game()
    {
        EnemyDeck = GiveDeckCart();
        PlayerDeck = GiveDeckCart();

    }

    List<Card> GiveDeckCart()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 30; i++)
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        return list;

    }

}

public class GameManagerScr : MonoBehaviour
{
    public Game currentGame;
    public Transform enemyHand, playerHand, enemyField, playerField;
    public GameObject CardPref;
    int turn, turnTime = 30;
    public TextMeshProUGUI turnTimeTxt;
    public Button EndTurnButton;
    public List<CardInfoScr> PlayerHandCards = new List<CardInfoScr>(),
                             PlayerFieldCards = new List<CardInfoScr>(),
                             EnemyHandCards = new List<CardInfoScr>(),
                             EnemyFieldCards = new List<CardInfoScr>();

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
        while (i++ < 4) GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0) return;

        Card card = deck[0];

        GameObject cardGO = Instantiate(CardPref, hand, false);
        if (hand == enemyHand)
        {
            cardGO.GetComponent<CardInfoScr>().HideCardInfo(card);
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScr>());
        }
        else
        {
            cardGO.GetComponent<CardInfoScr>().ShowCardInfo(card,true);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScr>());
            cardGO.GetComponent<CardForAttackScr>().enabled = false;
        }
        deck.RemoveAt(0);
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
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
        turnTimeTxt.SetText(turnTime.ToString());
        foreach (var card in PlayerFieldCards)
        {
            card.HideHighLightBG();
        }
        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            {
                card.SelfCard.ChangeAttackState(true);
                card.ShowHighLightBG();
            }
            while (turnTime-- > 0)
            {
                turnTimeTxt.text = turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            foreach (var card in EnemyHandCards)
            {
                card.SelfCard.ChangeAttackState(true);
            }
            while (turnTime-- > 27)
            {
                turnTimeTxt.text = turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            if (EnemyHandCards.Count > 0)
            {
                EnemyTurn(EnemyHandCards);
            }
        }

        ChangeTurn();
    }

    private void EnemyTurn(List<CardInfoScr> cards)
    {

        int count = Random.Range(0, cards.Count - 1);
        for (int i = 0; i < count; i++)
        {
            if (EnemyFieldCards.Count > 5) return;
            else
            {
                CardInfoScr currentCard = cards[0];
                currentCard.ShowCardInfo(cards[0].SelfCard,false);
                currentCard.transform.SetParent(enemyField);
                EnemyHandCards.Remove(currentCard);
                EnemyFieldCards.Add(currentCard);
            }
        }

    }
    public void CardsFight(CardInfoScr attackCard, CardInfoScr defenseCard)
    {
        attackCard.SelfCard.GetDamage(defenseCard.SelfCard.Attack);
        defenseCard.SelfCard.GetDamage(attackCard.SelfCard.Attack);
        if (!attackCard.SelfCard.IsAlive)
        {
            DestroyCard(attackCard);
        }
        else
        {
            attackCard.RefreshCardData();
        }
        if (!defenseCard.SelfCard.IsAlive)
        {
            DestroyCard(defenseCard);
        }
        else
        {
            defenseCard.RefreshCardData();
        }


    }
    public void DestroyCard(CardInfoScr card)
    {
        card.GetComponent<CardMovementScr>().OnEndDrag(null);
        if (EnemyFieldCards.Exists(x => x == card))
        {
            EnemyFieldCards.Remove(card);
        }
        if (PlayerFieldCards.Exists(x => x == card))
        {
            PlayerFieldCards.Remove(card);
        }
        Destroy(card.gameObject);
    }

}
