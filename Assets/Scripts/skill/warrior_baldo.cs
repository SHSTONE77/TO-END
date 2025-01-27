using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warrior_baldo : MonoBehaviour
{
    public GameObject Baldo_Player;
    public GameObject Baldo_Player2;
    public GameObject Baldo_Skill;
    public GameObject Baldo_Effect1;
    public GameObject Baldo_Effect2;
    public GameObject Baldo_Effect3;

    public Animator baldo_playerani;
    public Animator baldo_player2ani;
    public Animator baldo_skillani;
    public Animator baldo_effect1ani;
    public Animator baldo_effect2ani;
    public Animator baldo_effect3ani;

    public void player(Object obj)
    {
        Baldo_Player.SetActive(true);
    }
    public void player_ed()
    {
        Baldo_Player.SetActive(false);
    }
    public void player2(Object obj0)
    {
        Baldo_Player2.SetActive(true);
    }
    public void player2_ed()
    {
        Baldo_Player2.SetActive(false);
    }
    public void skill(Object obj1)
    {
        Baldo_Skill.SetActive(true);
    }
    public void skill_ed()
    {
        Baldo_Skill.SetActive(false);
    }

    public void skilleffect1(Object obj2)
    {
        Baldo_Effect1.SetActive(true);
    }
    public void skilleffect1_ed()
    {
        Baldo_Effect1.SetActive(false);
    }

    public void skilleffect2(Object obj3)
    {
        Baldo_Effect2.SetActive(true);
    }
    public void skilleffect2_ed()
    {
        Baldo_Effect2.SetActive(false);
    }

    public void skilleffect3(Object obj4)
    {
        Baldo_Effect3.SetActive(true);
    }
    public void skilleffect3_ed()
    {
        Baldo_Effect3.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        baldo_playerani = Baldo_Player.GetComponent<Animator>();
        baldo_player2ani = Baldo_Player2.GetComponent<Animator>();
        baldo_skillani = Baldo_Skill.GetComponent<Animator>();
        baldo_effect1ani = Baldo_Effect1.GetComponent<Animator>();
        baldo_effect2ani = Baldo_Effect2.GetComponent<Animator>();
        baldo_effect3ani = Baldo_Effect3.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
