using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragSlot : MonoBehaviour, IDropHandler
{   
    Image slotImage;
    
    void Start(){
        slotImage = gameObject.GetComponent<Image>();
    }

    // 오브젝트가 드롭되었을 시 호출되는 함수
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그한 오브젝트를 얻는다
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            slotImage = draggedObject.GetComponent<Image>();
        }
    }
}
