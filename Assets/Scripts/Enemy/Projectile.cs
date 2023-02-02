using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float sizeOverDistance = 0f;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if (sizeOverDistance > 0)
        {
            float size = Vector3.Distance(transform.position, _startPosition) * sizeOverDistance;
            transform.localScale = new Vector3(size, size, size);
        }
    }
}
