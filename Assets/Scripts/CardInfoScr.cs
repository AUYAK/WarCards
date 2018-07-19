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
    public GameObject HighLightBG;
    public bool IsPlayer;
    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(true);
        IsPlayer = false;
    }

    public void ShowCardInfo(Card card,bool isPlayer)
    {
        IsPlayer = isPlayer;
        SelfCard = card;
        HideObj.SetActive(false);
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
        RefreshCardData();
    }
    public void RefreshCardData()
    {
        Attack.text = SelfCard.Attack.ToString();
        Defense.text = SelfCard.Defense.ToString();
    }

    public void ShowHighLightBG()
    {
        HighLightBG.SetActive(true);
    }
    public void HideHighLightBG()
    {
        HighLightBG.SetActive(false);
    }
    private void Start()
    {
        //ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
