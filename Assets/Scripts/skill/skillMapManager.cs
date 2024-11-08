using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class skillMapManager : MonoBehaviour
{
    public player player;
    public TextMeshProUGUI user_skill_point;

    // Update is called once per frame
    void Update()
    {
        user_skill_point.text = player.skill_point.ToString();
    }
}
