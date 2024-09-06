using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButton : MonoBehaviour
{
    public void Click()
    {
        ScreenManager.instance.playerCode += 1;

        if (ScreenManager.instance.playerCode >= 4)
            ScreenManager.instance.playerCode = 1;
    }
}
