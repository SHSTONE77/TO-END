using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image; //이미지 관련 오류 땜빵용

public enum wa_skill
{
    teleport,
    fireball,
    heal,
    baldo,
    slash
}

//플레이어의 키와 그 키에 등록한 스킬을 저장하는 skillMap 테이블을 통해 입력한 키에 따른 스킬을 발동시키는 역할
public class wa_skillcon : MonoBehaviour, Iskillcon
{
    public GameObject warrior_baldo;
    Rigidbody2D rigid;
    Animator animator;  
    Stat stat;
    player Player;
    SpriteRenderer rend;    //flip을 위해 사용
    private bool isDamaging;    //damage를 줄 때 사용
    private float damagePercent;    //스킬의 배율을 설정
    public static Dictionary<int, wa_skill> skillMap = new Dictionary<int, wa_skill>();   //hashmap이 조회가 더 빠른데 c#에 없음 
    public Image[] coolTimeBox  = new Image[3];

    private void Start() 
    {
        //warrior_baldoani = warrior_baldo.GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        Player = gameObject.GetComponent<player>();
        stat = Player.stat;
        animator = gameObject.GetComponent<Animator>();
        skillMap.Add(0, wa_skill.teleport);
        //wa_con = GameObject.GetComponent<Animator>();

    }

    public Boolean HasSkill(int skillIndex){
        return skillMap.ContainsKey(skillIndex);
    }

    public void useSkill(int skillIndex){
        switch(skillMap[skillIndex]){  //스킬 추가 시 분기(case) 추가하고 아래에 메소드 작성, 스킬 애니메이션이 종료된 후 isInputBlocked는 true로 변경시켜야함
            case wa_skill.teleport : 
                StartCoroutine(player_tele());
                break;
            case wa_skill.baldo :
                baldo();
                break;
            default :
                ScreenManager.instance.setTextBox("구현되지 않은 스킬입니다");
                Debug.Log("존재하지 않는 스킬입니다.");
                break;
        }
        Player.isInputBlocked = false;
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

    private void baldo()
    {
        //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //    warrior_baldo.SetActive(true);
        //    animator.Play("warrior_baldo");
        //    float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - Player.transform.position.x;
        //    Vector3 moveTo = x > 0 ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        //animator.SetTrigger("isBaldo");
        //yield return new WaitForFixedUpdate();
        ////isDamaging = true;
        ////damagePercent = 2f;
        ////rend.flipX = x > 0 ? false : true;
        //transform.position += moveTo * 5;
        //animator.Play("Wa_standing");
        //while (stateInfo.normalizedTime < 2.1f)
        //{
        //    stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //    yield return new WaitForSeconds(0.1f);
        //}
        ////rend.flipX = false;
        //Player.isInputBlocked = false;
    }

    //private IEnumerable baldo()
    //{

    //}

    //데미지 처리
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
