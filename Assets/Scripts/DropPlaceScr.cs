using UnityEngine;
using UnityEngine.EventSystems;

public enum FIELD_TYPE
{
    SELF_HAND,
    SELF_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD
}

public class DropPlaceScr : MonoBehaviour, IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public FIELD_TYPE Type;
    public void OnDrop(PointerEventData eventData)
    {
        if (Type != FIELD_TYPE.SELF_FIELD) return;
            CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();
        if (card && card.GameManager.PlayerFieldCards.Count<6)
        {
            card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
            card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScr>());
            card.currentParent = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || Type==FIELD_TYPE.ENEMY_FIELD || Type == FIELD_TYPE.ENEMY_HAND) return;
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();
        if (card)
            card.defaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();
        if (card && card.defaultTempCardParent==transform)
            card.defaultTempCardParent = transform;
    }
}
