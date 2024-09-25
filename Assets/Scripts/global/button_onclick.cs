using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_onclick : MonoBehaviour
{
    public player player;
    public int playerCode;
    public GameObject stat_panel;
    public GameObject skill_panel;
    
    public void speed_up()
    {
        player.stat.moveSpeed += 1;
    }

    public void speed_down()
    {
        player.stat.moveSpeed -= 1;
    }

    public void attack_up()
    {
        player.stat.damage += 5;
    }

    public void attack_down()
    {
        player.stat.damage -= 5;
    }
    public void health_up()
    {
        player.stat.maxHp += 10;
        player.stat.curHp += 10;
    }

    public void health_down()
    {
        player.stat.maxHp -= 10;
    }
    public void StartGame() {
        ScreenManager.StartGame(playerCode);
    }

    public void statTab2skillTab() {
        stat_panel.SetActive(false);
        skill_panel.SetActive(true);
    }

    public void skillTab2statTab() {
        skill_panel.SetActive(false);
        stat_panel.SetActive(true);
    }

}
