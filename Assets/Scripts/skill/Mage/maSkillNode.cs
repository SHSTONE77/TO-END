using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newNode", menuName = "Skill Tree/MageSkillNode")]
public class maSkillNode : ScriptableObject
{
    public Sprite playSlotImage;
    public int cooltime;
    public ma_skill skillName; // 스킬 이름
    public bool isUnlocked = true; // 스킬 습득 여부
    public List<maSkillNode> preSkill;// 이전 스킬의 포인터
}
