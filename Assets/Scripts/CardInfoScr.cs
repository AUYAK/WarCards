﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoScr : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name;

    public void ShowInfoCard(Card card)
    {
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
    }

    private void Start()
    {
        ShowInfoCard(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}