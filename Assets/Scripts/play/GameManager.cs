using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject stat_panel;
    public player player;
    public static GameManager instance;
    bool isOpen;
    public Slider hpbar;
    [SerializeField]
    private TextMeshProUGUI maLastSkillPoints;
    [SerializeField]
    private TextMeshProUGUI waLastSkillPoints;
    [SerializeField]
    private TextMeshProUGUI enLastSkillPoints;
    [SerializeField]
    private GameObject wa_panel;
    [SerializeField]
    private GameObject ma_panel;
    [SerializeField]
    private GameObject en_panel;
    

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            isOpen = false;
            stat_panel.SetActive(isOpen);
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        handleSKillPoints();
    }

    public void cnt_stat(){
        if(isOpen){
            switch(ScreenManager.instance.playerCode){
                case 1 :
                    wa_panel.SetActive(false);
                    break;

                case 2 :
                    ma_panel.SetActive(false);
                    break;

                case 3 : 
                    en_panel.SetActive(false);
                    break;
            }
        }
        isOpen = !isOpen;       
        stat_panel.SetActive(isOpen);
    }
    
    public void handleHpBar(){
        hpbar.value = (float)player.stat.curHp / (float)player.stat.maxHp;
    }

    public void handleSKillPoints(){
        switch(ScreenManager.instance.playerCode){
            case 1 : 
                waLastSkillPoints.SetText(Convert.ToString(player.skill_point));
                break;
            case 2 : 
                maLastSkillPoints.SetText(Convert.ToString(player.skill_point));
                break;
            case 3 : 
                enLastSkillPoints.SetText(Convert.ToString(player.skill_point));
                break;
        }
    }

}