using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    float time;
    public GameObject character;

    // Update is called once per frame
    void Update()
    {
        if(time < 0.4f) //특정 위치에서 원점으로 이동
        {
            character.transform.position = new Vector3(0, -10 + 30 * time, 0);
        }
        else if (time < 0.5f) // 튕기고
        {
            character.transform.position = new Vector3(0, time+0.4f, 0)*4;
        }
        else if (time < 0.6f) //다시 제자리로
        {
            character.transform.position = new Vector3(0, 0.6f + time, 0)*4;
        }
        else if (time < 0.7f) //튕기고
        {
            character.transform.position = new Vector3(0, (time + 0.6f) /2, 0)*4;
        }
        else if (time < 0.8f) //다시 제자리
        {
            character.transform.position = new Vector3(0, 0.05f - (time + 0.7f)/2, 0)*4;
        }
        else
        {
            character.transform.position = Vector3.zero;
        }

        time += Time.deltaTime;

    }

}
