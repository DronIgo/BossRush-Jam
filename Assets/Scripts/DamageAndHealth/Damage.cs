using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Damage : MonoBehaviour
{
    public float damageAmount;
    [Tooltip("team ���������� �������, � ������� ��������� ����, ������� ������ - 0, ������ - 1")]
    public int team = 0;
    public UnityEvent OnDamageEvent = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            if (health.team != team && !health.invulnrable)
            {
                health.TakeDamage(damageAmount);
                OnDamageEvent.Invoke();
            }
        }
    }
}
