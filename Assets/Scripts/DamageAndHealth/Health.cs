using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Tooltip("team обозначает команду, к которой относится урон, команда игрока - 0, боссов - 1")]
    public int team = 0;
    public bool invulnrable = false;
    public float health = 10f;
    public UnityEvent OnDeathEvent = null;
    public UnityEvent OnDamageTaken = null;
    public void TakeDamage(float amount)
    {
        health -= amount;
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

    public void RestoreHealth(float amount, bool checkhealth = true)
    {
        if (health > 0 || !checkhealth)
            health = amount;
    }
}
