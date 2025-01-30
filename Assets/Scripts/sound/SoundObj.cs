using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObj : MonoBehaviour
{
    public AudioSource sound;

    private void Awake()
    {
        TryGetComponent(out sound);
    }
}
