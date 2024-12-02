using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance; // 전역 접근 가능 (싱글턴)

    public List<Quest> activeQuests = new List<Quest>();   // 진행 중인 퀘스트
    public List<Quest> completedQuests = new List<Quest>(); // 완료된 퀘스트

    private void Awake()
    {
        // 싱글턴 패턴: 오직 하나의 QuestManager만 존재하게 함
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환해도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest); // 새로운 퀘스트 추가
        Debug.Log($"새 퀘스트가 추가되었습니다: {quest.title}");
    }

    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            Debug.Log($"퀘스트 완료: {quest.title}");
        }
    }
}