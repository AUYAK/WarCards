using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoScr : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name,Attack,Defense;
    public GameObject HideObj;
    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(true);
    }

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(false);
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
        Attack.text = card.Attack.ToString();
        Defense.text = card.Defense.ToString();
    }

    private void Start()
    {
        //ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
