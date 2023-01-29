using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DragAndDrop)), RequireComponent(typeof(Animator))]
public class KeyUI : MonoBehaviour
{
    private DragAndDrop _dragAndDrop;
    private Vector3 _defaultPosition;
    private Animator _animator;
    [SerializeField] private KeyCode _key; 

    private void Awake()
    {
        _defaultPosition = transform.position;
        _dragAndDrop = GetComponent<DragAndDrop>();
        _dragAndDrop.OnEndDragEvent += ResetPosition;
        _dragAndDrop.OnDropEvent += SetAction;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InputManager.Instance.SubscribeToButton(PressButtonAnimation, _key);
    }

    private void ResetPosition(PointerEventData data)
    {
        transform.position = _defaultPosition;
    }

    private void SetAction(GameObject buttonObj)
    {
        ButtonUI buttonUI = buttonObj.GetComponent<ButtonUI>();
        ButtonUI.Button button = buttonUI.MyButton;
        PlayerController.Instance.SetAction(button, _key);
    }

    private void PressButtonAnimation()
    {
        _animator.SetTrigger("press");
    }
}
