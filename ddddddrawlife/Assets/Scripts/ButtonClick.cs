using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour
{
    public GameObject CollectionUI;
    public GameObject SettingUI;


    public void GameStart(){ // 게임 시작 버튼 
        SceneManager.LoadScene("NameScene");
    }

    public void Quit(){ // 게임 종료 버튼
        Application.Quit();
    }

    public void OpenCollection(){ // 도감 열기
        CollectionUI.SetActive(true);
    }

    public void CloseCollection(){ // 도감 닫기
        CollectionUI.SetActive(false);
    }

    public void OpenSetting(){
        SettingUI.SetActive(true);
    }

    public void CloseSetting(){
        SettingUI.SetActive(false);
    }

    public void MainBtn(){ // 메인으로 버튼
        SceneManager.LoadScene("MainScene");
    }

    public void QuizBtn(){
        if(EventSystem.current.currentSelectedGameObject.transform.Find("Text").gameObject.GetComponent<Text>().text == GameManager.GM.njob)
            GameManager.GM.CorF = 1; // 정답
        else
            GameManager.GM.CorF = 2;
    }

    public void MariaBtn(){ // 마리아 컷신 유년기 아이템 버튼
        GameObject btn = EventSystem.current.currentSelectedGameObject;
        int btn_idx = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex() - 1;

        //  이미 선택한 것을 선택하면 선택 취소
        if(CutsceneManager.CM.item2_idx.Contains(btn_idx)){
            CutsceneManager.CM.item2_idx.Remove(btn_idx);
            btn.GetComponent<Image>().color = new Color(1, 1, 1);
        } else{
            CutsceneManager.CM.item2_idx.Add(btn_idx);
            btn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

}
