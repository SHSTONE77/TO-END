using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum unitCode
{
    warrior,
    mage,
    engineer,
    zombie
}

//유닛의 스탯을 정의하는 클래스, 유닛코드를 통해 초기화하고 유닛코드를 제외한 스탯은 외부에서 호출가능
public class Stat     
{
    public unitCode unitCode {get;} //get은 생성자에서만 변경 가능, set은 지정해서 변경가능
    public string name {get;set;}
    public int maxHp{get;set;}
    public float curHp{get;set;}
    public int damage{get;set;}
    public float moveSpeed{get;set;}
    public float identity{get;set;}

    public Stat(){

    }

    //생성자 오버라이드
    public Stat(unitCode unitCode, string name, int maxHp, int damage, float moveSpeed, float identity){
        this.unitCode = unitCode;
        this.name = name;
        this.maxHp = maxHp;
        curHp = maxHp;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.identity = identity;
    }
    public Stat uni2Stat(unitCode unitCode){
        Stat stat = null;

        switch(unitCode){
            case unitCode.warrior : 
                stat = new Stat(unitCode, "warrior", 150, 30, 5f, 2f);
                break;
            case unitCode.mage : 
                stat = new Stat(unitCode, "mage", 100, 40, 3f, 5f);  
                break;
            case unitCode.engineer : 
                stat = new Stat(unitCode, "engineer", 115, 30, 4f, 6f);  
                break;
            case unitCode.zombie : 
                stat = new Stat(unitCode, "zombie", 90, 5, 2f, 3f);  
                break;
        }
        return stat;
    }
}
