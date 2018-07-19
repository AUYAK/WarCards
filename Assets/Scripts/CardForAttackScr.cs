using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardForAttackScr : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData)
    {
        CardInfoScr card = eventData.pointerDrag.GetComponent<CardInfoScr>();
        if (card && card.SelfCard.CanAttack && transform.parent == GetComponent<CardMovementScr>().GameManager.enemyField)
        {
            card.SelfCard.ChangeAttackState(false);
            if (card.IsPlayer)
            {
                card.HideHighLightBG();
            }
            GetComponent<CardMovementScr>().GameManager.CardsFight(card,GetComponent<CardInfoScr>());
        }
    }
}
