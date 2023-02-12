using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAfter : MonoBehaviour
{
    public GameObject toSummon;
    public float delay = 3f;
    float timeStart;
    bool summoned = false;
    private void Start()
    {
        timeStart = Time.time;        
    }

    private void Update()
    {
        if (Time.time - timeStart > delay)
        {
            if (!summoned)
            {
                Instantiate(toSummon, transform.position, Quaternion.identity);
                summoned = true;
            }
        }
    }
}
