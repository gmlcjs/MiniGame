using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
     [SerializeField] GameObject MenuPane;
     public string sceneValue="";

     public void OnClickMenu(){
        // 메뉴창 끄고 키기
        MenuPane.SetActive(!MenuPane.activeSelf); 

    }
    
    public void MainMove(){
        // 메인 씬이동
        SceneManager.LoadScene("MainScene");
    }

    public void RestartAndMove(){ // 다시시작 및 이동
        SceneManager.LoadScene(""+sceneValue);
    }


     // 게임 종료 함수
    public void QuitGameExit()
    {
        // 게임이 빌드된 상태에서 실행되는 경우 게임 종료
        #if UNITY_EDITOR
        // Unity Editor에서 테스트 중일 때는 게임을 종료하지 않고 플레이 모드를 종료
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 빌드된 게임에서는 종료
        Application.Quit();
        #endif
    }
}
