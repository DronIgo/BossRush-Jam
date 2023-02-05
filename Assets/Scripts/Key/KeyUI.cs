using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(DragAndDrop)), RequireComponent(typeof(Animator))]
public class KeyUI : MonoBehaviour
{
    private DragAndDrop _dragAndDrop;
    private Vector3 _defaultPosition;
    private Animator _animator;
    private Animator _imageAnimator;
    private Image _myImage;
    private Image _myImagesImage;

    public KeyCode Key; 
    

    private void Awake()
    {
        _defaultPosition = transform.position;
        _dragAndDrop = GetComponent<DragAndDrop>();
        _dragAndDrop.OnEndDragEvent += ResetPosition;
        _dragAndDrop.OnDropEvent += SetAction;
        _animator = GetComponent<Animator>();
        _imageAnimator = transform.Find("Image").gameObject.GetComponent<Animator>();
        _myImage = GetComponent<Image>();
        _myImagesImage = transform.Find("Image").gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        InputManager.Instance.SubscribeToButtonAnimation(PressButtonAnimation, Key);
    }

    private void ResetPosition(PointerEventData data)
    {
        transform.position = _defaultPosition;
    }

    private void SetAction(GameObject buttonObj)
    {
        ButtonUI buttonUI = buttonObj.GetComponent<ButtonUI>();
        ButtonUI.Button button = buttonUI.MyButton;
        PlayerController.Instance.SetAction(button, Key);
    }

    public void PressButtonAnimation()
    {
        _animator.SetTrigger("press");
    }

    public void Destroy()
    {
        StartCoroutine(DestroyOverTime());
    }

    IEnumerator DestroyOverTime()
    {
        _imageAnimator.SetBool("breaking", true);
        yield return new WaitForSeconds(3f);
        _myImage.color = new Color(1, 1, 1, 0.5f);
        _myImagesImage.color = new Color(1, 1, 1, 0.5f);
        _imageAnimator.SetBool("breaking", false);
        _dragAndDrop.enabled = false;
    }

    public void Revive()
    {
        _myImage.color = new Color(1, 1, 1, 1);
        _myImagesImage.color = new Color(1, 1, 1, 1);
        _imageAnimator.SetBool("breaking", false);
        _dragAndDrop.enabled = true;
    }
}
