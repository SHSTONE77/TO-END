using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 음식 오브젝트에 넣은 코드
public class dragButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector3 DefaultPos;
	
    // 드래그 시작
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
    	// 올바르지 않은 곳에 드래그 했을 때 돌아갈 위치를 저장해준다
        DefaultPos = this.transform.position; 
        // 드래그 시작 되었을 때는 드래그 중인 오브젝트의 레이캐스트 타겟을 꺼줘야 오류가 생기지 않는다
        GetComponent<Image>().raycastTarget = false; 
    }
	
    // 드래그 중
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    	transform.position = eventData.position;
    }
	
    // 드래그 끝
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
    	// 손님 오브젝트에서 음식 오브젝트에 대한 처리를 하고 오브젝트를 제거한다
        // 제거되지 않았다면 올바르지 않은 곳에 드롭된 것이기 때문에 원래 위치로 돌려준다
        this.transform.position = DefaultPos;
        // 레이캐스트 타겟도 원래대로 돌려준다
        GetComponent<Image>().raycastTarget = true;
    }
}
