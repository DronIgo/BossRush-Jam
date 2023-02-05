using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    void Update()
    {
        transform.up = _player.transform.position - transform.position;
    }
}
