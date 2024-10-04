using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// scene이 변경되어도 정보를 유지하는 싱글톤 객체
// 사실상 scene을 변경하는 역할보다는 game내의 정보를 관리하는 역할에 가까운 느낌이여서 개명해야할듯
public class ScreenManager : MonoBehaviour
{
    public int playerCode;
    public Dictionary<int, string> skillMap = new Dictionary<int, string>();
    public static ScreenManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.playerCode = 2;    //select_scene을 거치지 않고 test할 시 player를 척무진으로 설정
            instance.skillMap.Add(0, "teleport");  //keyset의 인덱스 0번을 텔레포트로 지정  
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public static void StartGame(int code) {
        instance.playerCode = code;
        SceneManager.LoadScene("play_scene");
    }
}
