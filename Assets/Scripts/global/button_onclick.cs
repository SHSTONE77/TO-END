using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_onclick : MonoBehaviour
{
    public player player;
    public int playerCode;
    // Update is called once per frame
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

    public void skill_scene_move() {
        ScreenManager.play2skill();
    }

    public void play_scene_move() {
        ScreenManager.skill2play();
    }

}
