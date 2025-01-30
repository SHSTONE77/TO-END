using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image; //이미지 관련 오류 땜빵용

public enum wa_skill
{
    skill3,
    skill2,
    baldo,
    dash
}

//플레이어의 키와 그 키에 등록한 스킬을 저장하는 skillMap 테이블을 통해 입력한 키에 따른 스킬을 발동시키는 역할
public class wa_skillcon : MonoBehaviour, Iskillcon
{
    Rigidbody2D rigid;
    Animator animator;  
    private player Player;
    SpriteRenderer rend;    //flip을 위해 사용
    public static Dictionary<int, wa_skill> skillMap = new Dictionary<int, wa_skill>();   //hashmap이 조회가 더 빠른데 c#에 없음 
    public Image[] coolTimeBox  = new Image[3];

    private void Start() {
        rend = GetComponent<SpriteRenderer>();
        Player = gameObject.GetComponent<player>();
        animator = gameObject.GetComponent<Animator>();
        skillMap.Add(0, wa_skill.dash);
    }

    public Boolean HasSkill(int skillIndex){
        return skillMap.ContainsKey(skillIndex);
    }

    public void useSkill(int skillIndex){
        switch(skillMap[skillIndex]){  //스킬 추가 시 분기(case) 추가하고 아래에 메소드 작성
            case wa_skill.dash : 
                StartCoroutine(player_tele());
                break;
            case wa_skill.baldo : 
                StartCoroutine(baldo());
                break;
            case wa_skill.skill2 : 
                StartCoroutine(skill2());
                break;
            case wa_skill.skill3 : 
                StartCoroutine(skill3());
                break;
            default :
                ScreenManager.instance.setTextBox("구현되지 않은 스킬입니다");
                Debug.Log("존재하지 않는 스킬입니다.");
                break;
        }
    }

    private int GetCurrentAnimationFrame()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0은 Base Layer
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        int totalFrames = Mathf.FloorToInt(clip.frameRate * clip.length);
        int currentFrame = Mathf.FloorToInt(stateInfo.normalizedTime * totalFrames);
        return currentFrame;
    }

    /**** 텔레포트 ****/
    private IEnumerator player_tele()  
    {   
        float delay = 1f;        
        Vector3 moveTo;
        Player.isInputBlocked = true;
        animator.SetInteger("direction", 0);
        animator.SetBool("isMoving", false);
        animator.SetBool("isDirChg", false);
        animator.Play("wa_standing");
        
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
        transform.position += moveTo * Player.stat.moveSpeed * Time.deltaTime * 1000;
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

        //플레이어 애니메이션 출력, 이펙트 출력
        Input.ResetInputAxes(); //왠진 모르겠지만 버그가 해결됨
        Player.isInputBlocked = true;
        yield return new WaitForSeconds(0.02f); //광클하면 발도 애니메이션이 바로 이동 애니메이션으로 전환되는 버그가 있어서 1프레임만큼 기다림
        
        //움직임 
        rend.flipX = x > 0 ? false : true;
        transform.position += moveTo * 4;
        
        animator.Play("wa_baldo");
        effectManager.Instance.HandleEffect("BaldoEffect", Player.stat.damage, new Vector2(transform.position.x, transform.position.y), rend.flipX);
        while(stateInfo.normalizedTime < 0.9f){
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(0.01f);
        }
        animator.Play("wa_standing");   //애니메이션 종료 후 idle상태로 전환
        
        //플레이어 위치 조정, flip 수정 및 움직임 활성화
        transform.position += moveTo * 5;
        rend.flipX = false;
        Player.isInputBlocked = false;
    }

    /**** 스킬 2 ****/
    private IEnumerator skill2()  
    {   
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - Player.transform.position.x;

        //플레이어 애니메이션 출력, 이펙트 출력
        Input.ResetInputAxes(); //왠진 모르겠지만 버그가 해결됨
        Player.isInputBlocked = true;
        yield return new WaitForSeconds(0.02f); //광클하면 발도 애니메이션이 바로 이동 애니메이션으로 전환되는 버그가 있어서 1프레임만큼 기다림
        
        //움직임 
        rend.flipX = x > 0 ? false : true;

        animator.Play("WarriorSkill2");

        // 5프레임째에서 이펙트 실행
        while (GetCurrentAnimationFrame() < 5)
        {
            yield return null;
        }
        effectManager.Instance.HandleEffect("WarriorSkill2Effect", Player.stat.damage, new Vector2(transform.position.x, transform.position.y), rend.flipX);

        while(stateInfo.normalizedTime < 0.9f){
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(0.01f);
        }
        animator.Play("wa_standing");   //애니메이션 종료 후 idle상태로 전환
        
        //flip 수정 및 움직임 활성화
        rend.flipX = false;
        Player.isInputBlocked = false;
    }

    /**** 스킬 3 ****/
    private IEnumerator skill3()  
    {   
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - Player.transform.position.x;

        //플레이어 애니메이션 출력, 이펙트 출력
        Input.ResetInputAxes(); //왠진 모르겠지만 버그가 해결됨
        Player.isInputBlocked = true;
        yield return new WaitForSeconds(0.02f); //광클하면 발도 애니메이션이 바로 이동 애니메이션으로 전환되는 버그가 있어서 1프레임만큼 기다림
        
        //움직임 
        rend.flipX = x > 0 ? false : true;

        animator.Play("WarriorSkill3");

        // 5프레임째에서 이펙트 실행
        while (GetCurrentAnimationFrame() < 5)
        {
            yield return null;
        }
        effectManager.Instance.HandleEffect("WarriorSkill3Effect", Player.stat.damage, new Vector2(transform.position.x, transform.position.y), rend.flipX);

        while(stateInfo.normalizedTime < 0.9f){
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(0.01f);
        }
        animator.Play("wa_standing");   //애니메이션 종료 후 idle상태로 전환
        
        //flip 수정 및 움직임 활성화
        rend.flipX = false;
        Player.isInputBlocked = false;
    }
  
    //   //데미지 처리
    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //    if(isDamaging){
    //         if(collision.CompareTag("Enemy")){
    //             IEnemy enemy = collision.GetComponent<IEnemy>();
    //             enemy.takeDamage(Player.stat.damage * damagePercent);
    //         }
    //    } 
    // }
}
