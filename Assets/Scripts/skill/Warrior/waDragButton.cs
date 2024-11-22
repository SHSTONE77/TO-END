using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//활성화 된 스킬버튼
public class waDragButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public waSkillNode skillNode;
    [SerializeField]
    private Image maskingBox;
    private Vector3 DefaultPos;
    private SkillTreeManager st;
    private Image slotImage;
    private Button selfButton;

    void Start(){
        st = FindObjectOfType<SkillTreeManager>();
        slotImage = GetComponent<Image>();
        slotImage.sprite = skillNode.playSlotImage;
        selfButton = GetComponent<Button>();
        selfButton.onClick.AddListener(OnSkillLearn);
    }

    void OnSkillLearn()
    {
        if(!skillNode.isUnlocked)
            return;
        
        st.TryUnlockWarriorskill(skillNode);
        if(skillNode.isUnlocked == false){
            Color currentColor = maskingBox.color;
            currentColor.a = 0.0f;  
            maskingBox.color = currentColor; //투명도를 0으로 변경한 maskingBox의 이미지
        }
    }

    //드래그 시작
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(skillNode.isUnlocked)
            return;

    	// 올바르지 않은 곳에 드래그 했을 때 돌아갈 위치를 저장해준다
        DefaultPos = this.transform.position; 
        // 드래그 시작 되었을 때는 드래그 중인 오브젝트의 레이캐스트 타겟을 꺼줘야 오류가 생기지 않는다
        GetComponent<Image>().raycastTarget = false; 
    }
	
    //드래그 중
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(skillNode.isUnlocked)
            return;

    	transform.position = eventData.position;
    }
	
    //드래그 끝
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if(skillNode.isUnlocked)
            return;

        this.transform.position = DefaultPos;
        GetComponent<Image>().raycastTarget = true;
    }
}
