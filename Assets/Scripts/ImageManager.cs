using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public Image CurrentImage;
    public Sprite Img_No0;
    public Sprite Img_No1;
    public Sprite Img_No2;

    void Update()
    {
        if (ScreenManager.instance.playerCode == 1)
            CurrentImage.sprite = Img_No0;
        else if (ScreenManager.instance.playerCode == 2)
            CurrentImage.sprite = Img_No1;
        else if (ScreenManager.instance.playerCode == 3)
            CurrentImage.sprite = Img_No2;
    }
    
}
