using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Start()
    {
        if (_player == null)
            _player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.up = _player.transform.position - transform.position;
    }
}
