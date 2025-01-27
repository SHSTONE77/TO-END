using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//시작 화면의 버튼 관리용
public class IntroScene : MonoBehaviour
{
    public void StartGame() {
        StartCoroutine(StartGame_co());
    }

    public void Option()
    {
        StartCoroutine(Option_co());
    }

    public void Intro()
    {
        StartCoroutine(Intro_co());
    }

    public void End()
    {
        Application.Quit();
    }

    private IEnumerator StartGame_co()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("TitleScene");
    }

    private IEnumerator Option_co()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("OptionScene");
    }

    private IEnumerator Intro_co()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameTitle");
    }
}
