using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Tooltip("team ���������� �������, � ������� ��������� ����, ������� ������ - 0, ������ - 1")]
    public int team = 0;
    public bool invulnrable = false;
    public float health = 10f;
    public UnityEvent OnDeathEvent = null;
    public UnityEvent OnDamageTaken = null;
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(amount);
        if (OnDamageTaken != null)
        {
            OnDamageTaken.Invoke();
        }
        if (health <= 0)
        {

            if (OnDeathEvent != null)
            {
                OnDeathEvent.Invoke();
            }
        }
    }

    public void RestoreHealth(float amount)
    {
        if (health > 0)
            health = amount;
    }
}
