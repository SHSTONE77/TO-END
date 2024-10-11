using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public interface IplayerMove{    //식별용 인터페이스
    public void useSkill(String skillName);
}

public class player : MonoBehaviour
{
    public bool isInputBlocked = false;
    Rigidbody2D rigid;
    unitCode unitCode;
    public Stat stat;
    /**** 애니메이션 컨트롤에 사용되는 변수 */
    /* direction : 1부터 4까지 반시계 방향으로 나타낸 방향(1:위쪽, 2:왼쪽, 3:아랫쪽, 4:오른쪽) */
    /* isDirChg : 방향의 전환 유무, */
    /* isMoving : 키 입력 여부(키를 누르고 있으면 True, 키를 뗀다면 False로 변경) */
    Animator animator;  
    public RuntimeAnimatorController anim_warrior;
    public RuntimeAnimatorController anim_mage;
    public RuntimeAnimatorController anim_engineer;
    int curDir;
    public GameManager game_manager;
    private int keyMax;
    public KeyCode[] keySet = new KeyCode[4];   //초기값은 object에서 설정
    IplayerMove playerSkill;
    public int stat_point;

    //실행 시 호출
    void Start()
    {
        keyMax = keySet.Length;
        //debug
        for(int i = 1; i < keyMax; i++){
            ScreenManager.instance.skillMap.Add(i, null);
        }
        animator = gameObject.GetComponent<Animator>();
        switch(ScreenManager.instance.playerCode){  //screen_manager에서 받아온 플레이어 코드에 따라 애니메이터와 스킬탭을 매핑
            case 1 :    //척무진
                unitCode = unitCode.warrior;
                animator.runtimeAnimatorController = anim_warrior;
                playerSkill = gameObject.AddComponent<warriorSkill>();
                break;

            case 2 :    //이청림
                unitCode = unitCode.mage;
                animator.runtimeAnimatorController = anim_mage;
                playerSkill = gameObject.AddComponent<mageSkill>();
                break;

            case 3 :    //설제관
                unitCode = unitCode.engineer;
                animator.runtimeAnimatorController = anim_engineer;
                playerSkill = gameObject.AddComponent<engineerSkill>();
                break;
        }
        curDir = 3;
        stat = new Stat();
        stat = stat.uni2Stat(unitCode);
    }

    //프레임 단위로 호출
    void Update()
    {     
        /*** 스탯창 전환 ***/
        if(Input.GetKeyDown(KeyCode.Tab)){
            game_manager.cnt_stat();
            isInputBlocked = !isInputBlocked;
        }

        if(isInputBlocked)  
            return;

        for(int i = 0; i < keyMax; i++){    //keyMax말고 keySet.Length 써도 되는데 update가 매 프레임마다 호출되다보니 변수를 써서 연산을 줄임  
            if (Input.GetKeyDown(keySet[i])){
                if(ScreenManager.instance.skillMap[i] == null){
                    ScreenManager.instance.setTextBox("등록된 스킬이 없습니다."); 
                    break;
                }
                curDir = 0;
                animator.SetBool("isDirChg", false);
                animator.SetBool("isMoving", false);
                isInputBlocked = true;
                playerSkill.useSkill(ScreenManager.instance.skillMap[i]);
                break;
            }  
        }
    }

    //Fixed Timestep에 따라 일정한 간격으로 호출
    void FixedUpdate(){
        game_manager.handleHpBar();
        /* 구르기, 공격, 스킬 시전 등 모션 중 입력을 받지 않는 행동 실행 시 True로 변환해 입력받지 않도록 처리 */
        if(isInputBlocked)  
            return;

        /**** 플레이어 이동 ****/
        //플레이어 입력 저장
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 0 && verticalInput == 0){    //입력값이 없는 경우
            animator.SetBool("isMoving", false);
        }
        else{
            animator.SetBool("isMoving", true);
            //방향 설정
            Vector2 moveTo = new Vector2(horizontalInput, verticalInput);
            int toDir = 0;  
            if(math.abs(horizontalInput) > math.abs(verticalInput)){
                if(horizontalInput > 0)
                    toDir = 4;
                else
                    toDir = 2;
            }
            else {
                if(verticalInput > 0)
                    toDir = 1;
                else
                    toDir = 3;
            }

            //애니메이션 변경
            if(curDir != toDir){    //방향의 변경이 이루어진 경우
                curDir = toDir;
                animator.SetInteger("direction", toDir);
                animator.SetBool("isDirChg", true);
            }
            else{
                animator.SetBool("isDirChg", false);
            }

            //위치변경(변경 위치 = 기존 위치 + 입력 값 * 상수)
            transform.position += new Vector3(moveTo.x, moveTo.y, 0f) * stat.moveSpeed * Time.deltaTime;
        }
    }

    //모든 update가 호출된 후, 마지막으로 호출
    void LateUpdate()
    {
        
    }

}