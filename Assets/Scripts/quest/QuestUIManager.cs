using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public GameObject questPanel; // QuestPanel 연결
    public Transform questListContainer; // Scroll View의 Content 부분
    public GameObject questItemPrefab; // QuestItemPrefab 연결

    private void Start()
    {
        questPanel.SetActive(false); // 처음엔 비활성화
    }

    public void ToggleQuestPanel()
    {
        questPanel.SetActive(!questPanel.activeSelf);
        if (questPanel.activeSelf)
        {
            UpdateQuestList();
        }
    }

    private void UpdateQuestList()
    {
        foreach (Transform child in questListContainer)
        {
            Destroy(child.gameObject); // 기존 항목 삭제
        }

        foreach (Quest quest in QuestManager.Instance.activeQuests)
        {
            GameObject questItem = Instantiate(questItemPrefab, questListContainer);
            questItem.GetComponent<Text>().text = quest.title;
        }
    }
}
