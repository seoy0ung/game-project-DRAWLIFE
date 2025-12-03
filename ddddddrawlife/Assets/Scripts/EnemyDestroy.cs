using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0)//자식이 모두 없어지면 파괴
            Destroy(gameObject);
    }
}
