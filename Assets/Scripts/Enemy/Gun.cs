using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool shot = false;
    public GameObject projectile = null;
    private void Update()
    {
        if (shot)
        {
            shot = false;
            if (projectile != null)
                Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}
