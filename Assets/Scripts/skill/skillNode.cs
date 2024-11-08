using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum button_code
{
    passive,
    active
}

public class skillNode : MonoBehaviour
{
    [SerializeField]
    private button_code button_type;
    private Image outer_box;

    private Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if(GameManager.instance.player.skill_point > 1){
            outer_box.color = new Color(0, 0, 0);
            switch(button_type) {
                case button_code.passive :
                    gameObject.AddComponent<dragButton>();
                    break;
                case button_code.active :
                    break;
            }
        }
        else{
            ScreenManager.instance.setTextBox("스킬 포인트가 모자랍니다.");
        }
    }
}