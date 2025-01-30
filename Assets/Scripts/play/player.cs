using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public interface Iskillcon{    //식별용 인터페이스
    public virtual Boolean HasSkill(int skillIndex){
        return true;
    }
    public virtual void useSkill(int skillIndex){}   // virtual : 자식 클래스에서의 재정의(override)를 허용하는 옵션, 이유가 있었는데 사라짐 
}

public class player : MonoBehaviour
{
    //오디오=================================================
    [SerializeField] private AudioClip footstep;
    [SerializeField] private new AudioSource audio; //발소리 제어용
    private float footTime = 0f;
    private float soundLen = 2f;

    //애니메이션==============================================
    [SerializeField] private unitCode unitCode;
    [SerializeField] private RuntimeAnimatorController anim_warrior;
    [SerializeField] private RuntimeAnimatorController anim_mage;
    [SerializeField] private RuntimeAnimatorController anim_engineer;
    private Animator animator;
    
    //스킬==================================================
    [SerializeField] private Image[] coolTimeBox;
    [SerializeField] private KeyCode[] keySet;   //인스펙터창에서 keycode 지정이 필요, 추후에 키세팅 개발 시 이용
    public bool isInputBlocked = false;
    public static Dictionary<int, int> cooltimeManager = new Dictionary<int, int>();
    public List<float> skillCooldown = new List<float>();
    Iskillcon playerSkill;

    //플레이어 정보==========================================
    public Stat stat;
    int curDir;
    private int keyMax;
    public int stat_point;
    public int skill_point;

    //실행 시 호출
    void Start()
    {
        keyMax = keySet.Length;
        cooltimeManager.Add(0, 5);  //index 0번에 들어가는 대쉬의 쿨타임 설정
        for(int i = 0; i < keyMax; i++){
            skillCooldown.Add(0f);
        }
        animator = gameObject.GetComponent<Animator>();
        switch(ScreenManager.instance.playerCode){  //screen_manager에서 받아온 플레이어 코드에 따라 애니메이터와 스킬탭을 매핑
            case 1 :    //척무진
                unitCode = unitCode.warrior;
                animator.runtimeAnimatorController = anim_warrior;
                playerSkill = gameObject.AddComponent<wa_skillcon>();
                break;

            case 2 :    //이청림
                unitCode = unitCode.mage;
                animator.runtimeAnimatorController = anim_mage;
                playerSkill = gameObject.AddComponent<ma_skillcon>();
                break;

            case 3 :    //설제관
                unitCode = unitCode.engineer;
                animator.runtimeAnimatorController = anim_engineer;
                playerSkill = gameObject.AddComponent<en_skillcon>();
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
            GameManager.instance.cnt_stat();
        }

        if(!isInputBlocked)  {
            for(int i = 0; i < keyMax; i++){    //keyMax말고 keySet.Length 써도 되는데 update가 매 프레임마다 호출되다보니 변수를 써서 연산을 줄임  
                if (Input.GetKeyDown(keySet[i])){
                    if(playerSkill.HasSkill(i)){    //미등록 상황 예외처리
                        if(coolTimeBox[i].fillAmount < 0.97f){  //쿨타임 중인 경우
                            ScreenManager.instance.setTextBox("스킬이 아직 준비되지 않았습니다");
                        }
                        else{
                            StartCoroutine(coolTime(i));
                            animator.SetBool("isDirChg", false);
                            animator.SetBool("isMoving", false);
                            playerSkill.useSkill(i);
                            break;
                        }
                    }
                    else{
                        ScreenManager.instance.setTextBox("등록된 스킬이 없습니다");
                    }
                }  
            }
        }
    }

    //Fixed Timestep에 따라 일정한 간격으로 호출
    void FixedUpdate(){
        GameManager.instance.handleHpBar();
        /* 구르기, 공격, 스킬 시전 등 모션 중 입력을 받지 않는 행동 실행 시 True로 변환해 입력받지 않도록 처리 */
        if(!isInputBlocked){  

            /**** 플레이어 이동 ****/
            //플레이어 입력 저장
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput == 0 && verticalInput == 0){    //입력값이 없는 경우
                animator.SetBool("isMoving", false);
                audio.Stop();
                footTime = 0f;
            }
            else{
                if(footTime < Time.time){
                    audio.clip = footstep;
                    audio.Play();
                    footTime = Time.time + soundLen;
                }
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
    }

    //모든 update가 호출된 후, 마지막으로 호출
    void LateUpdate()
    {

    }

    //쿨타임 적용
    private IEnumerator coolTime(int slotSeq){
        skillCooldown[slotSeq] =  0;
        while(skillCooldown[slotSeq] < cooltimeManager[slotSeq]-0.01f){   
            skillCooldown[slotSeq] += Time.deltaTime;
            coolTimeBox[slotSeq].fillAmount = skillCooldown[slotSeq]/cooltimeManager[slotSeq];
            yield return new WaitForFixedUpdate();
        }
        coolTimeBox[slotSeq].fillAmount = 1f;
    }
}