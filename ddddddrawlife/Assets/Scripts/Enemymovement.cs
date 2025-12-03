using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymovement : MonoBehaviour
{
    public float speed = 15f;//스피드 조정(아마 GameManager에 넣어야할듯..)
    private void Start()
    {
       StartCoroutine(EnemyDestroy());//장애물 파괴 코루틴 시작
    }
    private void Update()
    {
        gameObject.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);//오른쪽에서 왼쪽으로 이동
    }
    IEnumerator EnemyDestroy()//장애물이 파괴되는 코루틴 :: 조건 1. 화면 왼쪽으로 넘어갈때, 조건 2. 플레이어와 닿일때
    {
        while (true)
        {
            if (gameObject.transform.position.x < -40)//파괴되는 위치는 조정가능
            {
                if(transform.parent){ // 부모가 있는경우 (enemy3,4)
                    Destroy(transform.parent.gameObject);
                    Debug.Log("파괴");
                    Debug.Log(transform.parent);
                }
                else{
                    Destroy(gameObject);
                }
                yield return null;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    //플레이어와 닿으면 파괴되는 조건
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")//플레이어 tag를 Player로 설정
        {
            Destroy(gameObject);
        }
    }
}
