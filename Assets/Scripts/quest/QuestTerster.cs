using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTester : MonoBehaviour
{
    private void Start()
    {
        // 새로운 퀘스트 생성
        Quest firstQuest = new Quest("첫 번째 퀘스트", "이것은 첫 번째 퀘스트입니다!");
        Quest secondQuest = new Quest("두 번째 퀘스트", "주변적을 처치하세요!");

        // 퀘스트를 QuestManager에 추가
        QuestManager.Instance.AddQuest(firstQuest);
        QuestManager.Instance.AddQuest(secondQuest);

        // 퀘스트 목록 출력
        Debug.Log("현재 진행 중인 퀘스트 목록:");
        foreach (Quest quest in QuestManager.Instance.activeQuests)
        {
            Debug.Log($"퀘스트 제목: {quest.title}, 퀘스트 설명: {quest.description}");
        }

        // 첫 번째 퀘스트를 완료
        QuestManager.Instance.CompleteQuest(firstQuest);
        Debug.Log("첫 번째 퀘스트 완료 처리!");

        // 완료된 퀘스트 확인
        Debug.Log("완료된 퀘스트 목록:");
        foreach (Quest quest in QuestManager.Instance.completedQuests)
        {
            Debug.Log($"퀘스트 제목: {quest.title}, 퀘스트 설명: {quest.description}");
        }
    }
}