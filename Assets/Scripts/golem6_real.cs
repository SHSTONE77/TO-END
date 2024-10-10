using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem6_real : MonoBehaviour
{
    public float Speed = 2.01f;
    public Transform Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 direction = Player.position - transform.position;
        direction.Normalize();

        transform.position += direction * Speed * Time.deltaTime;
    }
}