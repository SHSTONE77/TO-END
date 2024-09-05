using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public interface playerMove{    //식별용 인터페이스
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
    public ScreenManager screen_manager;

    public Dictionary<int, string> skillMap = new Dictionary<int, string>(); // 단축키테이블의 인덱스 - 스킬을 매핑시켜주는 관계테이블, 시작 시 keySet에 정적으로 정의된 키로 초기화 후 스킬탭에서 할당
    
    public KeyCode[] keySet = new KeyCode[3];   //키설정 기능만들때 keySet이랑 skillMap의 key값 수정해야함

    playerMove playerSkill;

    //실행 시 호출
    void Start()
    {
        //테스트용으로 스페이스, q, e로 설정
        keySet[0] = KeyCode.Space;
        keySet[1] = KeyCode.Q;
        keySet[2] = KeyCode.E;

        animator = gameObject.GetComponent<Animator>();
        Debug.Log(screen_manager.playerCode);
        switch(screen_manager.playerCode){  //screen_manager에서 받아온 플레이어 코드에 따라 애니메이터와 스킬탭을 매핑

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
        animator.SetBool("isDelay", false);
    }

    //프레임 단위로 호출
    void Update()
    {
        
        /*** 스탯창 전환 ***/
        if(Input.GetKeyDown(KeyCode.Tab)){
            game_manager.cnt_stat();
            isInputBlocked = !isInputBlocked;
        }

        /*** 텔레포트 시작 ***/
        if (Input.GetKey(keySet[0])){
            playerSkill.useSkill(skillMap[0]);
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
