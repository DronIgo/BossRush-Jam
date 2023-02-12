using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float lifeDuration = 5f;

    private float timeStart;
    void Start()
    {
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeStart > lifeDuration)
            Destroy(gameObject);
    }
}
