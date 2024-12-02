using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 유니티에서 이 클래스의 내용을 직렬화해서 보여줌
public class Quest
{
    public string title;       // 퀘스트 제목
    public string description; // 퀘스트 설명
    public bool isCompleted;   // 퀘스트 완료 여부

    // 생성자: 새로운 퀘스트를 만들 때 사용
    public Quest(string title, string description)
    {
        this.title = title;
        this.description = description;
        this.isCompleted = false; // 처음에는 완료되지 않은 상태
    }
}
