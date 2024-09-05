using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using String = System.String;

public class engineerSkill : MonoBehaviour, playerMove
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    Animator animator;  
    Stat stat;

    player Player;

    private void Start() {
        Player = gameObject.GetComponent<player>();
        Player.skillMap.Add(0, "teleport");  //keyset의 인덱스 0번을 텔레포트로 지정  
        animator = gameObject.GetComponent<Animator>();
        Debug.Log("sdf");
    }

    public void useSkill(String skillName){
        switch(skillName){

            case "teleport" : 
                Player.isInputBlocked = true;
                StartCoroutine(player_tele());
                Player.isInputBlocked = false;
                break;
            default :
                Debug.Log("존재하지 않는 스킬입니다.");
                break;

        }
    }

    private IEnumerator player_tele()
    {   
        Debug.Log("teleporting");
        float delay = 0.01f;
        
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
        animator.SetBool("isTele", true);
        animator.SetBool("isDirChg", false);

        //delay동안  키입력을 막고 무적상태
        while(animator.GetBool("isTele")==true){
            yield return new WaitForSeconds(delay);
        }
        animator.SetBool("isDelay", false);
        animator.SetBool("isTele", false);
        transform.position += moveTo * stat.moveSpeed * Time.deltaTime * 3;
        //동작완료 0.05초 후 이동가능
        yield return new WaitForSeconds(0.2f);
    }
}
