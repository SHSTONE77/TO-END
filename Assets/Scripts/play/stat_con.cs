using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class stat_con : MonoBehaviour
{
    public player player;
    public TextMeshProUGUI user_dmg;
    public TextMeshProUGUI user_hp;
    public TextMeshProUGUI user_spd;

    // Update is called once per frame
    void Update()
    {
        user_dmg.text = player.stat.damage.ToString();
        user_hp.text = player.stat.maxHp.ToString();
        user_spd.text = player.stat.moveSpeed.ToString();
    }
}
