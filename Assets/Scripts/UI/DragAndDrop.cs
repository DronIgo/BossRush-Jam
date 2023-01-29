using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    public event Action<PointerEventData> OnBeginDragEvent;
    public event Action<PointerEventData> OnEndDragEvent;
    public event Action<GameObject> OnDropEvent;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        _canvasGroup.blocksRaycasts = false;
        if (OnBeginDragEvent != null)
            OnBeginDragEvent.Invoke(data);
    }

    public void OnEndDrag(PointerEventData data)
    {
        _canvasGroup.blocksRaycasts = true;
        if (OnEndDragEvent != null)
            OnEndDragEvent.Invoke(data);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnDrop(GameObject handler)
    {
        if (OnDropEvent != null)
            OnDropEvent.Invoke(handler);
    }
}
