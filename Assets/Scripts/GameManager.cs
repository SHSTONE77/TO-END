using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject stat_panel;
    public player player;
    public static GameManager instance;
    bool isOpen;
    public Slider hpbar;

    void Start()
    {
        isOpen = false;
        stat_panel.SetActive(isOpen);    
        instance = this;
    }

    public void cnt_stat(){
        isOpen = !isOpen;
        stat_panel.SetActive(isOpen);
    }
    
    public void handleHpBar(){
        hpbar.value = (float)player.stat.curHp / (float)player.stat.maxHp;
    }

}