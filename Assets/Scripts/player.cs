using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{

    bool isInputBlocked = false;
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

    //실행 시 호출
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Debug.Log(screen_manager.playerCode);
        switch(screen_manager.playerCode){
            case 1 :    //척무진
                unitCode = unitCode.warrior;
                animator.runtimeAnimatorController = anim_warrior;
                break;

            case 2 :    //이청림
                unitCode = unitCode.mage;
                animator.runtimeAnimatorController = anim_mage;
                break;

            case 3 :    //설제관
                unitCode = unitCode.engineer;
                animator.runtimeAnimatorController = anim_engineer;
                break;
        }
        curDir = 0;
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

        /*** 텔레포트 시작 ***/
        if (Input.GetKey(KeyCode.Space)){
            StartCoroutine(player_tele());
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

    /**** 텔레포트 ****/
    /* 시전 중 isInputBlocked를 true로 만들어 키 입력을 막음 */
    /* layer를 변경시켜 충돌처리를 적용치않음 */
    /* isMoving : 키 입력 여부(키를 누르고 있으면 True, 키를 뗀다면 False로 변경) */
    private IEnumerator player_tele()
    {   
        float delay = 1.0f;
        Vector3 moveTo;

        switch(curDir){
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
        isInputBlocked = true;
        animator.SetBool("isTele", true);
        animator.SetBool("isDirChg", false);
        //delay동안  키입력을 막고 무적상태
        yield return new WaitForSeconds(delay);
        animator.SetBool("isTele", false);
        transform.position += moveTo * stat.moveSpeed * Time.deltaTime * 10;
        //동작완료 0.05초 후 이동가능
        yield return new WaitForSeconds(0.2f);
        isInputBlocked = false;
    }
}
