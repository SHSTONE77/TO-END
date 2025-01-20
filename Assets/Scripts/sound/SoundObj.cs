using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObj : MonoBehaviour
{
    public AudioSource audio;

    private void Awake()
    {
        TryGetComponent(out audio);
    }
}
