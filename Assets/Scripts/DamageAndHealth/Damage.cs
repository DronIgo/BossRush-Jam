using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Damage : MonoBehaviour
{
    public float damageAmount;
    [Tooltip("team ���������� �������, � ������� ��������� ����, ������� ������ - 0, ������ - 1")]
    public int team = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            if (health.team != team)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
