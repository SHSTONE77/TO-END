using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public Rigidbody2D target;
    

    bool isLive = true;

    Rigidbody2D rigid;
    public unitCode unitCode;
    public Stat stat;
    Animator animator;  
    Boolean isAttack;
    
    public player player;

    //생성자
    void Start()
    {
        isAttack = false;
        animator = gameObject.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        stat = new Stat();
        stat = stat.uni2Stat(unitCode);    
    }

    //충돌판정
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")){
            return;
        }    

        stat.curHp -= collision.GetComponent<player>().stat.damage;

        if(stat.curHp > 0){//생존

        }
        else {
            Dead();
        }
    }

    void Dead(){
        gameObject.SetActive(false);
    }

    //정해진 시간 기반으로 호출(update는 프레임 단위)
    void FixedUpdate()
    {
        if(!isLive || isAttack) //죽으면(isLive = false) 동작 X
            return;
        Vector2 dirVec = target.position - rigid.position;  //타겟과 enemy의 위치 차이를 계산하고 .normalized를 통해 1로 정규화하여 방향으로 사용
        float player_far;
        if(math.abs(dirVec.x) > math.abs(dirVec.y)){
            player_far = math.abs(dirVec.x);
            if(dirVec.x > 0){
                animator.SetInteger("direction", 4);
            }
            else{
                animator.SetInteger("direction", 2);
            }
        }
        else{
            player_far = math.abs(dirVec.y);
            if(dirVec.y > 0){
                animator.SetInteger("direction", 1);
            }
            else{
                animator.SetInteger("direction", 3);
            }
        }
        Vector2 nextVec = dirVec.normalized * stat.moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);   //현재 위치 + 현재 이동 거리(FixedDeltaTime동안의)를 더한 값으로 위치를 변경
        // rigid.velocity = Vector2.zero;  //충돌 시 물리로 튕겨져나가는 값을 (0, 0)으로 설정
        if (player_far < 1.4){
            StartCoroutine(golem_attack(animator.GetInteger("direction")));
        }
    }

    private IEnumerator golem_attack(int dir){
        isAttack = true;
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.8f);
        switch(dir){
            case 1 : 
                if(Math.Abs(target.position.x - rigid.position.x) <  0.4){
                    if((target.position.y - rigid.position.y) < 1.8)
                        player.stat.curHp -= stat.damage;
                }        
                break;

            case 2 : 
                if(Math.Abs(target.position.y - rigid.position.y) <  0.4){
                    if((rigid.position.x - target.position.x) < 1.8)
                        player.stat.curHp -= stat.damage;
                }        
                break;

            case 3 : 
                if(Math.Abs(target.position.x - rigid.position.x) <  0.4){
                    if((rigid.position.y - target.position.y) < 1.8)
                        player.stat.curHp -= stat.damage;
                }        
                break;

            case 4 : 
                if(Math.Abs(target.position.y - rigid.position.y) <  0.4){
                    if((target.position.x - rigid.position.x) < 1.8)
                        player.stat.curHp -= stat.damage;
                }        
                break;
            
        }
        yield return new WaitForSeconds(0.3f);
        isAttack = false;
        animator.SetBool("isAttack", false);
    }

    public void takeDamage(float damage){
        stat.curHp -= damage;
        //추후에 피격처리 추가
    }

}
