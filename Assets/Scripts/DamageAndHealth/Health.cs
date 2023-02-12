using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Tooltip("team обозначает команду, к которой относится урон, команда игрока - 0, боссов - 1")]
    public int team = 0;
    public float invulnDuration = 0f;
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

    public bool invulnrableDamage = false;

    public void BecomeInvulnFromDamage()
    {
        invulnrableDamage = true;
        StartCoroutine(ResetInvuln());
    }

    IEnumerator ResetInvuln()
    {
        yield return new WaitForSeconds(invulnDuration);
        invulnrableDamage = false;
    }

    public void RestoreHealth(float amount)
    {
        if (health > 0)
            health = amount;
    }

    public void ResetHealth(float amount)
    {
        health = amount;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
