using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum en_skill
{
    teleport,
    fireball,
    heal,
    thunder
}

public class en_skillcon : MonoBehaviour, Iskillcon
{
    Rigidbody2D rigid;
    Animator animator;  
    Stat stat;
    player Player;
    public static Dictionary<int, en_skill> skillMap = new Dictionary<int, en_skill>();
    public static Dictionary<int, int> cooltimeManager = new Dictionary<int, int>();

    private void Start() {
        Player = gameObject.GetComponent<player>();
        stat = Player.stat;
        animator = gameObject.GetComponent<Animator>();
        skillMap.Add(0, en_skill.teleport);
    }

    public Boolean HasSkill(int skillIndex){
        return skillMap.ContainsKey(skillIndex);
    }

    public void useSkill(int skillIndex){
        animator.SetBool("isDirChg", false);
        animator.SetBool("isMoving", false);

        switch(skillMap[skillIndex]){  //스킬 추가 시 분기(case) 추가하고 아래에 메소드 작성
            case en_skill.teleport : 
                StartCoroutine(player_tele());
                break;
            default :
                Debug.Log("존재하지 않는 스킬입니다.");
                break;

        }
    }

    /**** 텔레포트 ****/
    private IEnumerator player_tele()  
    {   
        float delay = 1f;
        
        Vector3 moveTo;

        switch(animator.GetInteger("direction")){
            case 1 : 
                moveTo = new Vector3(0, 1, 0);
                break;
            case 2 : 
                moveTo = new Vector3(-1, 0, 0);
                break;
            case 3 : 
                moveTo = new Vector3(0, -1, 0);
                break;
            case 4 : 
                moveTo = new Vector3(1, 0, 0);
                break;
            default : //게임 실행 시를 제외하고는 적용되지 않는 예외케이스
                moveTo = new Vector3(0, 0, 0);
                break;
        }
        Player.isInputBlocked = true;
        yield return new WaitForSeconds(delay);
        transform.position += moveTo * stat.moveSpeed * Time.deltaTime * 10;
        //동작완료 0.05초 후 이동가능
        yield return new WaitForSeconds(0.2f);
        Player.isInputBlocked = false;
    }
}
