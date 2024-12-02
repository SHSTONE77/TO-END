using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField]
    private player player;
    public List<maSkillNode> maSkills;
    public List<enSkillNode> enSkills;
    public List<waSkillNode> waSkills;

    void Start()
    {
        //스킬트리를 시작 시 전부 잠금된 상태로 초기화. 추후에 외부 파일 불러들여서 초기화하는 식으로 변경
        for(int i = 0; i < maSkills.Count; i++){
            maSkills[i].isUnlocked = true;
        }
        for(int i = 0; i < enSkills.Count; i++){
            enSkills[i].isUnlocked = true;
        }
        for(int i = 0; i < waSkills.Count; i++){
            waSkills[i].isUnlocked = true;
        }
    }

    public void TryUnlockMageskill(maSkillNode skill)
    {
        if(player.skill_point < 0){
            ScreenManager.instance.setTextBox("스킬 포인트가 부족합니다.");
            return;
        }

        if (!skill.isUnlocked)
        {
            //버튼을 삭제하는 방식으로 구현해 들어올 일 없는 케이스
            ScreenManager.instance.setTextBox("이미 습득된 스킬입니다.");
            return;
        }

        // 선행 조건 확인
        foreach (var prerequisite in skill.preSkill)
        {
            if (prerequisite.isUnlocked)
            {
                ScreenManager.instance.setTextBox("사전 스킬을 획득해야합니다.");
                return;
            }
        }

        // 조건 충족 시 습득
        skill.isUnlocked = false;
        player.skill_point--;
        GameManager.instance.handleSKillPoints();
        Debug.Log("스킬을 습득했습니다!");
    }

    public void TryUnlockWarriorskill(waSkillNode skill)
    {
        if(player.skill_point < 0){
            ScreenManager.instance.setTextBox("스킬 포인트가 부족합니다.");
            return;
        }

        if (!skill.isUnlocked)
        {
            //버튼을 삭제하는 방식으로 구현해 들어올 일 없는 케이스
            ScreenManager.instance.setTextBox("이미 습득된 스킬입니다.");
            return;
        }

        // 선행 조건 확인
        foreach (var prerequisite in skill.preSkill)
        {
            if (prerequisite.isUnlocked)
            {
                ScreenManager.instance.setTextBox("사전 스킬을 획득해야합니다.");
                return;
            }
        }

        // 조건 충족 시 습득
        skill.isUnlocked = false;
        player.skill_point--;
        GameManager.instance.handleSKillPoints();
        Debug.Log("스킬을 습득했습니다!");
    }

    public void TryUnlockEngineerskill(enSkillNode skill)
    {
        if(player.skill_point < 0){
            ScreenManager.instance.setTextBox("스킬 포인트가 부족합니다.");
            return;
        }

        if (!skill.isUnlocked)
        {
            //버튼을 삭제하는 방식으로 구현해 들어올 일 없는 케이스
            ScreenManager.instance.setTextBox("이미 습득된 스킬입니다.");
            return;
        }

        // 선행 조건 확인
        foreach (var prerequisite in skill.preSkill)
        {
            if (prerequisite.isUnlocked)
            {
                ScreenManager.instance.setTextBox("사전 스킬을 획득해야합니다.");
                return;
            }
        }

        // 조건 충족 시 습득
        skill.isUnlocked = false;
        player.skill_point--;
        GameManager.instance.handleSKillPoints();
        Debug.Log("스킬을 습득했습니다!");
    }
}