using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem2A : MonoBehaviour
{
    public float Speed = 2f;
    public float CoolTime = 3f;
    public float InvisibleTime = 1f;
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
            InvisibleTime -= Time.deltaTime;
            Img_Renderer.color = new Color32(255, 255, 255, 0);

            if (InvisibleTime <= 0)
            {
                InvisibleTime = 1f;
                Img_Renderer.color = new Color32(255, 255, 255, 255);
                CoolTime = 3f;
            }
        }
    }
}
