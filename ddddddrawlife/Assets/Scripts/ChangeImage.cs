using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public GameObject Check;//체크박스
    public GameObject CImage;//캐릭터 이미지
    public void ImageChange() //선택 여부에 따라 이미지 반투명 or 투명 조절
    {
        if (Check.GetComponent<Toggle>().isOn == true) //선택 된 경우
        {
            Color Icolor = CImage.GetComponent<Image>().color;
            Icolor.a = 1f;
            CImage.GetComponent<Image>().color = Icolor;
        }//투명도 1 -> 선명하게
        else //선택되지 앟은 경우
        {
            Color Icolor = CImage.GetComponent<Image>().color;
            Icolor.a = 0.5f;
            CImage.GetComponent<Image>().color = Icolor;
        }//투명도 0.5 -> 반투명하게
    }
}
