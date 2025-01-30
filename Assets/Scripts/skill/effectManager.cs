using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectManager : MonoBehaviour
{
    public static effectManager Instance;
    [SerializeField] effect[] effects; // 자식 오브젝트 참조

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (var effect in effects)
            {
                effect.gameObject.SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //인자로 (이펙트 애니메이션의 이름, 공격력, 이펙트가 출력될 절대좌표)를 받음
    public void HandleEffect(string skillName, float damage, Vector2 position, bool isRend)    //skillName으로 써두긴 했는데 경우에 따라서는 보스나 맵 기믹의 이펙트 처리를 해도 좋을듯?
    {
        foreach (var effect in effects)
        {
            // 비활성화된 이펙트를 찾아 활성화
            if (!effect.gameObject.activeSelf)
            {
                effect.transform.position = new Vector3(position.x, position.y, 0);
                effect.ActivateEffect(skillName, damage, isRend);
                return;
            }
        }
        Debug.Log("접근할 수 있는 이펙트 오브젝트가 존재하지 않습니다.");
    }
}