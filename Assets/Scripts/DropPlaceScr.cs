using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScr : MonoBehaviour, IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();
        if (card) card.currentParent = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
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
