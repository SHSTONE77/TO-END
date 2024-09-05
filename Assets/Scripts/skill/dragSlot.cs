using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image; //이미지 관련 오류 땜빵용

public class dragSlot : MonoBehaviour, IDropHandler
{   
    Image slotImage;
    public int slotSeq;
    public player Player;
    
    void Start(){
        slotImage = GetComponent<Image>();
    }

    // 오브젝트가 드롭되었을 시 호출되는 함수
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그한 오브젝트를 얻는다
        GameObject draggedObject = eventData.pointerDrag;
        Debug.Log("aa");
        if (draggedObject != null)
        {
            dragButton dragData = draggedObject.GetComponent<dragButton>();
            slotImage.sprite = dragData.skillImage;
            Player.skillMap.Add(slotSeq, dragData.skillName);
        }
    }
}
