using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public int playerCode;
    public static ScreenManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.playerCode = 2;    //select_scene을 거치지 않고 test할 시 player를 이청림으로 설정
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

    public static void play2skill() {
        SceneManager.LoadScene("skill_scene");
    }

    public static void skill2play() {
        SceneManager.LoadScene("play_scene");
    }
}
