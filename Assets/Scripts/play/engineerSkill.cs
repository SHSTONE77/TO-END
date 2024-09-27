using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class engineerSkill : MonoBehaviour, playerMove
{
    Rigidbody2D rigid;
    Animator animator;  
    Stat stat;

    player Player;

    private void Start() {
        Player = gameObject.GetComponent<player>();
        stat = Player.stat;
        animator = gameObject.GetComponent<Animator>();
    }

    public void useSkill(String skillName){
        switch(skillName){  //스킬 추가 시 분기(case) 추가하고 아래에 메소드 작성
            case "teleport" : 
                StartCoroutine(player_tele());
                break;
            default :
                Debug.Log("존재하지 않는 스킬입니다.");
                break;

        }
    }

    /**** 텔레포트 ****/
    /* 시전 중 isInputBlocked를 true로 만들어 키 입력을 막음 */
    /* layer를 변경시켜 충돌처리를 적용치않음 */
    /* isMoving : 키 입력 여부(키를 누르고 있으면 True, 키를 뗀다면 False로 변경) */
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
        animator.SetBool("isTele", true);
        animator.SetBool("isDirChg", false);

        // //delay동안  키입력을 막고 무적상태, 나중에 애니메이션 이벤트에서 isTele를 false로 변경하는 기능 적용 후에 적용
        // while(animator.GetBool("isTele")==true){
        //     yield return new WaitForSeconds(delay);
        // }

        yield return new WaitForSeconds(delay);
        animator.SetBool("isDelay", false);
        animator.SetBool("isTele", false);
        transform.position += moveTo * stat.moveSpeed * Time.deltaTime * 10;
        //동작완료 0.05초 후 이동가능
        yield return new WaitForSeconds(0.2f);
        Player.isInputBlocked = false;
    }
}
