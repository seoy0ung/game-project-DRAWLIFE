using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 달리는 듯한 느낌 내기 위해 땅을 움직이는 코드
public class GroundMove : MonoBehaviour
{
    public float speed = 3f; // 달리기 속도
    public GameObject[] grounds;

    float ground_length = 11.8f; // 한 조각의 길이

    void Update()
    {  
        for (int i = 0; i < 3; i++)
        {
            grounds[i].transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

            if (grounds[i].transform.position.x < -16)
            {
                float nextX = grounds[i].transform.position.x;
                nextX = nextX + ground_length * 3;
                grounds[i].transform.position = new Vector3(nextX, -4, 0);
            };
        }
    }
}
