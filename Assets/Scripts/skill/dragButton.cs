using System;
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
    public String skillName;
    public Sprite skillImage;   //slot에서 사용하기 위해 public으로 생성
    Vector3 DefaultPos;
	
    void Start(){
        skillImage = gameObject.GetComponent<Sprite>();
    }

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
        //test
        Debug.Log("sdf");

    	transform.position = eventData.position;
    }
	
    // 드래그 끝
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = DefaultPos;
        GetComponent<Image>().raycastTarget = true;
    }
}
