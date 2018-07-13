using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Camera mainCamera;
    Vector3 offset;
    public Transform currentParent, defaultTempCardParent;
    GameObject tempCardGO;

    public void Awake()
    {
        mainCamera = Camera.allCameras[0];
        tempCardGO = GameObject.Find("TempCardGO");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - mainCamera.ScreenToWorldPoint(eventData.position);
        currentParent = defaultTempCardParent = transform.parent;

        tempCardGO.transform.SetParent(currentParent);
        tempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(currentParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = mainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;

        if (tempCardGO.transform.parent != defaultTempCardParent)
            tempCardGO.transform.SetParent(defaultTempCardParent);

        CheckCardPosititon();
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(currentParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(tempCardGO.transform.GetSiblingIndex());

        tempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        tempCardGO.transform.SetPositionAndRotation(new Vector3(2340, 0), new Quaternion());

         

    }
    private void CheckCardPosititon()
    {
        int index = defaultTempCardParent.childCount;
        for (int i = 0; i < defaultTempCardParent.childCount; i++)
        {
            if (transform.position.x < defaultTempCardParent.GetChild(i).transform.position.x)
            {
                index = i;
                if (tempCardGO.transform.GetSiblingIndex() < index)
                {
                    index--;
                }
                break;
            }
        }
        tempCardGO.transform.SetSiblingIndex(index);
    }
}
