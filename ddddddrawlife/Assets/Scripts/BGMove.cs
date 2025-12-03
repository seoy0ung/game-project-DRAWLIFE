using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGMove : MonoBehaviour
{
    public float speed = 3f; // 달리기 속도

    //float BG_length = 35.1f; // 한 조각의 길이

    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

        if(transform.position.x < -25 && GameManager.GM.nstate != 2)
        {
            
            transform.localPosition = new Vector3(37.6f, 1.6f, 0);
        }

        if (transform.position.x < -30 && GameManager.GM.nstate == 2)
        {
       
             transform.localPosition = new Vector3(43f, 1.6f, 0);
  
        }


    }
}
