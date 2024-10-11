using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButton : MonoBehaviour
{
    public void Click()
    {
        ScreenManager.instance.playerCode -= 1;

        if (ScreenManager.instance.playerCode <= 0)
            ScreenManager.instance.playerCode = 3;
    }
}
