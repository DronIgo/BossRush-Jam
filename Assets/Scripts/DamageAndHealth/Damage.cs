using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public float damageAmount;
    [Tooltip("team обозначает команду, к которой относится урон, команда игрока - 0, боссов - 1")]
    public int team = 0;
    public bool dealDamageOnStay = false;
    public UnityEvent OnDamageEvent = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            if (health.team != team && !health.invulnrable && !health.invulnrableDamage)
            {
                health.TakeDamage(damageAmount);
                OnDamageEvent.Invoke();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!dealDamageOnStay) return;
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            if (health.team != team && !health.invulnrable && !health.invulnrableDamage)
            {
                health.TakeDamage(damageAmount);
                OnDamageEvent.Invoke();
            }
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
