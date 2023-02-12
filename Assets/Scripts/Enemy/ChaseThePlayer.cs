using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChaseThePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float speed = 2f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        if (_player == null)
            _player = GameObject.Find("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _rigidbody.position += (Vector2)direction * speed * Time.deltaTime;
    }
}
