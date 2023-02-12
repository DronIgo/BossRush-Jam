using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    public UnityEvent onPickup = new UnityEvent();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            KeyUIManager.Instance.EnableKey();
            onPickup.Invoke();
            Destroy(gameObject);
        }
    }
}
