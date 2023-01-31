using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Общий скрипт для элекментов, на которые можно что-то перетащить (например для кнопок действий)
/// </summary>
public class DropHandler : MonoBehaviour, IDropHandler
{
    public delegate void DropHandle (GameObject obj);
    public event DropHandle OnDropEvent;
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragAndDrop>().OnDrop(gameObject);
    }
}
