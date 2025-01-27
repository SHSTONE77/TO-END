using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{
    public Slider musicbar;

    private void Update()
    {
        BgmManager.Instance.musicSource.volume = musicbar.value;
    }
}
