using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

[CreateAssetMenu(fileName = "newNode", menuName = "Skill Tree/MageSkillNode")]
public class maSkillNode : ScriptableObject
{
    public Sprite playSlotImage;
    public int cooltime;
    public ma_skill skillName; // 스킬 이름
    public bool isUnlocked = true; // 스킬 습득 여부
    public List<maSkillNode> preSkill;// 이전 스킬의 포인터
}

[CreateAssetMenu(fileName = "newNode", menuName = "Skill Tree/EngineerSkillNode")]
public class enSkillNode : ScriptableObject
{
    public Sprite playSlotImage;
    public int cooltime;
    public en_skill skillName; // 스킬 이름
    public bool isUnlocked = true; // 스킬 습득 여부
    public List<enSkillNode> preSkill; // 이전 스킬의 포인터
}

[CreateAssetMenu(fileName = "newNode", menuName = "Skill Tree/WarriorSkillNode")]
public class waSkillNode : ScriptableObject
{
    public Sprite playSlotImage;
    public int cooltime;
    public wa_skill skillName; // 스킬 이름
    public bool isUnlocked = true; // 스킬 습득 여부
    public List<waSkillNode> preSkill; // 이전 스킬의 포인터
}
