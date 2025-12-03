using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeText : MonoBehaviour
{
    public Text QuizJobName; //프리펩에 속해있는 텍스트 ui
    public GameObject BG; //프리펩에 속해있는 배경화면
    private RectTransform Rtransform;
    private void Awake() 
    {
        Rtransform = QuizJobName.rectTransform;
        QuizJobName.text = GameManager.GM.njob;
    }
    private void Update() // UI가 배경화면을 따라다니게
    {
        Rtransform.position = Camera.main.WorldToScreenPoint(new Vector3(BG.transform.position.x, BG.transform.position.y, 0));
    }

}
