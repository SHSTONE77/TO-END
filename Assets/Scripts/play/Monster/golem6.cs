using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem6 : MonoBehaviour
{
    public float Speed = 2f;
    float TransP = 255f;
    public SpriteRenderer Img_Renderer;
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

        if (TransP >= 128)
            TransP *= 0.9999f;

        Img_Renderer.color = new Color32(255, 255, 255, (byte)TransP);
    }
}
