using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetting : MonoBehaviour
{
    public float num;
    public void CSetting(int idx) // 유년기 아이템
    {
        // 수치 저장
        num = GameManager.GM.ncharacter.childGauge[idx];
        // 각 수치에 따라 프리팹에 tag 달아주기
        if(num > 0)
            gameObject.tag = "ITEMPLUS";
        else
            gameObject.tag = "ITEMMINUS";
    }

    public void SSetting(int n) // 학생기 아이템
    {
        // 수치 저장
        num = n;
        if(num > 0){
            gameObject.tag = "ITEMPLUS";
            num *= 1;
        }
        else{
            gameObject.tag = "ITEMMINUS";
            num *= 3;
        }
    }
}