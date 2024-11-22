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

public class maDragSlot : MonoBehaviour, IDropHandler
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
            maDragButton dragData = draggedObject.GetComponent<maDragButton>();
            if(ma_skillcon.skillMap.ContainsKey(slotSeq))  //skillmap의 slotSeq번째 자리에 이미 등록된 스킬이 존재하는 경우
            {
                ma_skillcon.skillMap[slotSeq] = dragData.skillNode.skillName;
                player.cooltimeManager[slotSeq] = dragData.skillNode.cooltime;
            }
            else{   //등록된 스킬이 없는 경우
                ma_skillcon.skillMap.Add(slotSeq, dragData.skillNode.skillName);
                player.cooltimeManager.Add(slotSeq, dragData.skillNode.cooltime);
            }
            slotImage.sprite = dragData.skillNode.playSlotImage; //스킬트리 스킬 슬롯의 이미지를 드래그한 이미지로 변경
            playSlotImage.sprite = dragData.skillNode.playSlotImage; //인게임 스킬 슬롯의 이미지를 드래그한 이미지로 변경
        }
    }
}
