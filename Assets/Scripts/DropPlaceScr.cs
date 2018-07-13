using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScr : MonoBehaviour, IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardScr card = eventData.pointerDrag.GetComponent<CardScr>();
        if (card) card.currentParent = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        CardScr card = eventData.pointerDrag.GetComponent<CardScr>();
        if (card)
            card.defaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        CardScr card = eventData.pointerDrag.GetComponent<CardScr>();
        if (card && card.defaultTempCardParent==transform)
            card.defaultTempCardParent = transform;
    }
}
