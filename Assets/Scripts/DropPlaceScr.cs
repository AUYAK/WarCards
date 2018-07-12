using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScr : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardScr card = eventData.pointerDrag.GetComponent<CardScr>();
        if (card) card.currentParent = transform;
    }
}
