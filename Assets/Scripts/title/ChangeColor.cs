using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Image Select_Button;
    
    void Update()
    {
        if (ScreenManager.instance.playerCode == 1)
            Select_Button.color = new Color32(255, 128, 128, 255);
        else if (ScreenManager.instance.playerCode == 2)
            Select_Button.color = new Color32(128, 255, 255, 255);
        else if (ScreenManager.instance.playerCode == 3)
            Select_Button.color = new Color32(255, 255, 128, 255);
    }
}
