using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem3 : MonoBehaviour
{
    public float Speed = 2f;
    public float FasterSpeed = 10f;
    public float CoolTime = 4f;
    public float FasterTime = 1f;
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

        if (CoolTime > 0)
            CoolTime -= Time.deltaTime;
        else
        {
            Speed = FasterSpeed;
            FasterTime -= Time.deltaTime;
            Img_Renderer.color = new Color32(255, 0, 0, 255);

            if (FasterTime <= 0)
            {
                Speed = 2f;
                Img_Renderer.color = new Color32(255, 255, 255, 255);
                FasterTime = 1f;
                CoolTime = 4f;
            }
        }
    }
}
