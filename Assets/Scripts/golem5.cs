using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem5 : MonoBehaviour
{
    public float Speed = 2f;
    public float CoolTime = 3f;
    public float BurfTime = 3f;
    public float NurfTime = 3f;
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
        else {
            BurfTime -= Time.deltaTime;

            if (BurfTime > 0)
                Img_Renderer.color = new Color32(255, 0, 0, 255);
            else {
                NurfTime -= Time.deltaTime;

                if(NurfTime > 0)
                    Img_Renderer.color = new Color32(0, 0, 255, 255);
                else {
                    BurfTime = 3f;
                    NurfTime = 3f;
                }
            }
        }
    }
}
