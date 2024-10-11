using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text CurrentText;
    
    void Update()
    {
        if (ScreenManager.instance.playerCode == 1)
            CurrentText.text = "<color=#FF8080>" + "ô �� ��" + "</color>";
        else if (ScreenManager.instance.playerCode == 2)
            CurrentText.text = "<color=#80FFFF>" + "�� û ��" + "</color>";
        else if (ScreenManager.instance.playerCode == 3)
            CurrentText.text = "<color=#FFFF80>" + "�� �� ��" + "</color>";
    }
}
