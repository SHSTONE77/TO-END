using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HP_MN : MonoBehaviour
{
    public int maxhp;
    public int currenthp;

    // Start is called before the first frame update
    void Start()
    {
        currenthp = maxhp;
    }

    public void hpmanager(int increase, int decrease)
    {

        if (currenthp <= 0)
        {
            //플레이어 사망
        }


        else if (currenthp > 0) //플레이어가 생존 시 동작 함
        {
            currenthp -= decrease; //데이지 먼저 받고 치유 받도록 순서를 정했음
            currenthp += increase; 

            if(currenthp > maxhp) //플레이어 현재 체력이 최대 체력을 넘어가지 않도록
            {
                currenthp = maxhp;
            }
        }
    }
    
}
