using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public SpriteRenderer Img_Renderer;
    public Sprite Sprite0;
    public Sprite Sprite1;
    public Sprite Sprite2;

    public void GameStart()
    {

    }

    void Update()
    {
        if (ScreenManager.instance.playerCode == 1)
            Img_Renderer.sprite = Sprite0;
        else if (ScreenManager.instance.playerCode == 2)
            Img_Renderer.sprite = Sprite1;
        else if (ScreenManager.instance.playerCode == 3)
            Img_Renderer.sprite = Sprite2;
    }
}
