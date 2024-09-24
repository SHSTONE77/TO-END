using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image; //이미지 관련 오류 땜빵용

public class dragSlot : MonoBehaviour, IDropHandler
{   
    Image slotImage;    //슬롯의 주소를 저장하는 역참조역할
    Image playSlotImage;
    public int slotSeq;
    public GameObject playSlot;
        
    void Start(){
        slotImage = GetComponent<Image>();
        playSlotImage = playSlot.GetComponent<Image>();
    }

    // 오브젝트가 드롭되었을 시 호출되는 함수
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그한 오브젝트를 얻는다
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            dragButton dragData = draggedObject.GetComponent<dragButton>();
            slotImage.sprite = dragData.skillImage; //슬롯의 이미지를 드래그한 이미지로 변경
            playSlotImage.sprite = dragData.skillImage;
            if(ScreenManager.instance.skillMap.ContainsKey(slotSeq))
            {
                ScreenManager.instance.skillMap[slotSeq] = dragData.skillName;
            }
            else{
                ScreenManager.instance.skillMap.Add(slotSeq, dragData.skillName);
            }
        }
    }
}
