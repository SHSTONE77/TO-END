using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  버튼관련 메서드를 다 때려박은 class. 
//  나중에 사용될 버튼을 대략적으로 캔버스 active와 관련된 버튼, 스탯분배용 버튼,  
//  save, load와 같은 scene전환용 버튼으로 구분하여 다르게 관리해야할듯
public class button_onclick : MonoBehaviour
{
    public player player;
    public GameObject stat_panel;

    [SerializeField]
    private GameObject wa_skill;
    [SerializeField]
    private GameObject ma_skill;
    [SerializeField]
    private GameObject en_skill;
    
    public void speed_up()
    {
        if(player.stat_point > 0){
            player.stat_point -= 1;
            player.stat.moveSpeed += 1;
        }  //추후에 else에 스탯포인트가 없다는 오류메세지 출력
    }
    public void speed_down()
    {
        player.stat_point += 1;
        player.stat.moveSpeed -= 1;
    }

    public void attack_up()
    {
        if(player.stat_point > 0){
            player.stat_point -= 1;
            player.stat.damage += 5;
        }
    }

    public void attack_down()
    {
        player.stat_point += 1;
        player.stat.damage -= 5;
    }
    public void health_up()
    {
        if(player.stat_point > 0){
            player.stat_point -= 1;
            player.stat.maxHp += 10;
            player.stat.curHp += 10;
        }
    }

    public void health_down()
    {
        player.stat_point += 1;
        player.stat.maxHp -= 10;
    }
    public void StartGame() {
        ScreenManager.StartGame();
    }

    public void statTab2skillTab() {
        stat_panel.SetActive(false);
        switch(ScreenManager.instance.playerCode){
            case 1 :
                wa_skill.SetActive(true);
                break;

            case 2 :
                ma_skill.SetActive(true);
                break;

            case 3 : 
                en_skill.SetActive(true);
                break;
        }
    }

    public void skillTab2statTab() {
        stat_panel.SetActive(true);
        switch(ScreenManager.instance.playerCode){
            case 1 :
                wa_skill.SetActive(false);
                break;

            case 2 :
                ma_skill.SetActive(false);
                break;

            case 3 : 
                en_skill.SetActive(false);
                break;
        }
    }

}
