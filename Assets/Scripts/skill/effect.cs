using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    private SpriteRenderer rend;
    private float damage;
    private float duration;
    private Animator animator;

    private void Awake()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    //effect를 활성화하고 입력받은 effect애니메이션을 실행, 플레이어의 공격력으로 damage를 초기화
    public void ActivateEffect(string skillName, float damage, bool isRend)
    {
        Debug.Log($"ActivateEffect 호출됨: {skillName}, damage: {damage}, isRend: {isRend}");

        // 이펙트를 활성화, 이펙트 비활성화는 종료 프레임에서 실행
        gameObject.SetActive(true);

        this.damage = damage;
        rend.flipX = isRend;

        // 지정된 이름의 애니메이션 재생
        animator.Play(skillName);

    }

    //애니메이션 이벤트는 애니메이션이 박혀있는 오브젝트에 박혀있는 객체 인스턴스에서 함수를 찾음, 인자는 종류별로 1개만 전달할 수 있고 string으로 대충 다 넘겨주고 파싱하는 식으로 우회는 가능
    //애니메이션 이벤트 데미지 처리용(damageP는 데미지의 계수, force는 경직을 얼마나 줄 지)
    //aa.bbbbb = > aa => force, 0.bbbbb => b.bbbb(damageP)  , ex : 4.024 = 4의 force, 2.4의 데미지지계수
    public void TriggerEffect(float combinedValue)
    {

        int force = Mathf.FloorToInt(combinedValue); // 정수 부분 = force
        float damageP = (combinedValue - force) * 10; // 소수 부분 = damageP

        Debug.Log(damageP*damage);

        Collider2D myCollider = GetComponent<Collider2D>(); // Custom Physics Shape Collider
        ContactFilter2D filter = new ContactFilter2D();
        Collider2D[] hitEnemies = new Collider2D[10]; // 최대 10개의 적 감지

        int count = myCollider.OverlapCollider(filter, hitEnemies);
        for (int i = 0; i < count; i++)
        {
            if (hitEnemies[i].CompareTag("Enemy"))
            {
                hitEnemies[i].GetComponent<Enemy>().takeDamage(damageP*damage);
            }
        }
    }

    //애니메이션 이벤트 종료용
    public void EffectEnd(){
        gameObject.SetActive(false);
        rend.flipX = false;
    }
}