using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answermove : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Translate(new Vector3(-1, 0, 0) * 8 * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//플레이어 tag를 Player로 설정
        {
            if (transform.parent)
            { // 부모가 있는경우 (enemy3,4)
                Destroy(transform.parent.gameObject);
                Debug.Log("정답제출 완료");
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("정답제출 완료");
            }
        }
    }
}
