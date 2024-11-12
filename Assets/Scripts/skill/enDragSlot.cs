using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image; //이미지 관련 오류 땜빵용

public class enDragSlot : MonoBehaviour, IDropHandler
{   
    Image slotImage;    //슬롯의 주소를 저장하는 역참조역할
    Image playSlotImage;
    public int slotSeq;
    public GameObject playSlot;
        
    void Start(){
        slotImage = GetComponent<Image>();
        playSlotImage = playSlot.GetComponent<Image>();
    }

    //오브젝트가 드롭되었을 시 호출
    public void OnDrop(PointerEventData eventData)
    {
        //드래그한 오브젝트의 정보를 저장
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            enDragButton dragData = draggedObject.GetComponent<enDragButton>();
            if(en_skillcon.skillMap.ContainsKey(slotSeq))  //skillmap의 slotSeq번째 자리에 이미 등록된 스킬이 존재하는 경우
            {
                en_skillcon.skillMap[slotSeq] = dragData.skillName;
                player.cooltimeManager[slotSeq] = dragData.cooltime;
            }
            else{   //등록된 스킬이 없는 경우
                en_skillcon.skillMap.Add(slotSeq, dragData.skillName);
                player.cooltimeManager.Add(slotSeq, dragData.cooltime);
            }
            slotImage.sprite = dragData.skillImage; //스킬트리 스킬 슬롯의 이미지를 드래그한 이미지로 변경
            playSlotImage.sprite = dragData.skillImage; //인게임 스킬 슬롯의 이미지를 드래그한 이미지로 변경
        }
    }
}
