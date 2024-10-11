using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class warriorSkill : MonoBehaviour, IplayerMove
{
    Rigidbody2D rigid;
    Animator animator;  
    Stat stat;
    player Player;
    SpriteRenderer rend;    //flip을 위해 사용
    private bool isDamaging;    //damage를 줄 때 사용
    private float damagePercent;    //스킬의 배율을 설정
    

    private void Start() {
        rend = GetComponent<SpriteRenderer>();
        Player = gameObject.GetComponent<player>();
        stat = Player.stat;
        animator = gameObject.GetComponent<Animator>();
    }

    public void useSkill(String skillName){
        switch(skillName){  //스킬 추가 시 분기(case) 추가하고 아래에 메소드 작성
            case "teleport" : 
                StartCoroutine(player_tele());
                break;
            case "baldo" : 
                StartCoroutine(baldo());
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
        yield return new WaitForSeconds(delay);
        transform.position += moveTo * stat.moveSpeed * Time.deltaTime * 1000;
        //동작완료 0.05초 후 이동가능
        yield return new WaitForSeconds(0.2f);
        Player.isInputBlocked = false;
    }

    /**** 발도 ****/
    /* 마우스 포인터를 기준으로 발동 */
    /* 1. 마우스 포인터의 위치, 플레이어의 위치를 기준으로 방향을 계산 */
    /* 2. 방향으로 애니메이션을 발동 */
    /* 3. 애니메이션이 끝날때까지 데미지처리를 하고 끝나는 시점에 움직임을 활성화 */
    private IEnumerator baldo()  
    {   
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - Player.transform.position.x;
        Vector3 moveTo = x > 0 ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        isDamaging = true;
        damagePercent = 2f;

        rend.flipX = x > 0 ? false : true;
        transform.position += moveTo * 4;
        animator.Play("wa_baldo");
        while(stateInfo.normalizedTime < 0.9f){
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(0.01f);
        }
        animator.Play("wa_standing");   //애니메이션 종료 후 idle상태로 전환
        transform.position += moveTo * 5;
        rend.flipX = false;
        Player.isInputBlocked = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if(isDamaging){
            if(collision.CompareTag("Enemy")){
                IEnemy enemy = collision.GetComponent<IEnemy>();
                enemy.takeDamage(Player.stat.damage * damagePercent);
            }
       } 
    }
}
