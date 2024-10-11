using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem4 : MonoBehaviour
{
    public float Speed = 2f;
    public float RushSpeed = 15f;
    public float CoolTime = 4f;
    public float RushCoolTime = 1f;
    public Transform Player;
    public SpriteRenderer Img_Renderer;
    public Vector3 RushToThisPos;
    Vector3 direction;
    float XPosDifference;
    float YPosDifference;
    public float PosDifference;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        transform.position += direction * Speed * Time.deltaTime;

        if (CoolTime > 0)
        {
            direction = Player.position - transform.position;
            direction.Normalize();

            CoolTime -= Time.deltaTime;
            RushToThisPos = Player.position;
        }
        else
        {
            RushCoolTime -= Time.deltaTime;
            Speed = 0;
            float color = RushCoolTime * 255;
            
            if (color < 0)
                color = 0;

            Img_Renderer.color = new Color32(255, (byte)color, (byte)color, 255);

            if (RushCoolTime <= 0)
            {
                Speed = RushSpeed;
                direction = RushToThisPos - transform.position;
                direction.Normalize();

                XPosDifference = transform.position.x - RushToThisPos.x < 0 ? -(transform.position.x - RushToThisPos.x) : transform.position.x - RushToThisPos.x;
                YPosDifference = transform.position.y - RushToThisPos.y < 0 ? -(transform.position.y - RushToThisPos.y) : transform.position.y - RushToThisPos.y;

                PosDifference = Mathf.Sqrt(Mathf.Pow(XPosDifference, 2) + Mathf.Pow(YPosDifference, 2));

                if (PosDifference <= 0.01)
                {
                    direction = Player.position - transform.position;
                    Img_Renderer.color = new Color32(255, 255, 255, 255);
                    Speed = 2f;
                    CoolTime = 4f;
                    RushCoolTime = 1f;
                }
            }
        }
    }
}
