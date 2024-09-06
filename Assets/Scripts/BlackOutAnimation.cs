using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOutAnimation : MonoBehaviour
{
    public GameObject Black_Out_Curtain_Object;
    public Image Black_Out_Curtain;
    float Black_Out_Curtain_value;
    float Black_Out_Curtain_speed;

    void Start()
    {
        Black_Out_Curtain_value = 1;
        Black_Out_Curtain_speed = 0.75f;
    }

    void Update()
    {
        if (Black_Out_Curtain_value > 0)
            HideBlackOut_Curtain();
        if (Black_Out_Curtain_value <= 0)
            Destroy(Black_Out_Curtain_Object);
    }

    public void HideBlackOut_Curtain()
    {
        Black_Out_Curtain_value -= Time.deltaTime * Black_Out_Curtain_speed;
        Black_Out_Curtain.color = new Color(0.0f, 0.0f, 0.0f, Black_Out_Curtain_value);
    }
}
