using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool shot = false;
    private bool shouldShot = false;
    public GameObject projectile = null;

    //Не спрашивайте
    private void Update()
    {
        if (!shot)
        {
            shouldShot = true;
        }
        if (shot)
        {
            if (projectile != null && shouldShot)
                Instantiate(projectile, transform.position, transform.rotation);
            shouldShot = false;
        }
    }
}
