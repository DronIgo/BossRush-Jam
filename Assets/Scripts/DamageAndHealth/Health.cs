using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int team = 0;
    public float health = 10f;
    public UnityEvent OnDeathEvent = null;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            if (OnDeathEvent != null)
            {
                OnDeathEvent.Invoke();
            }
        }
    }
}
