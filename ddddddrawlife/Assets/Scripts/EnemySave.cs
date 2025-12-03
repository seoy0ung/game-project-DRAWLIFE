using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    public bool patternSave = false;
    // Start is called before the first frame update
    void Start()
    {
        if (patternSave) // save가 true일때만 패턴 저장
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy1");
            GameObject[] itemList = GameObject.FindGameObjectsWithTag("Item_toy");

            int len_enemy = enemyList.Length;
            int len_item = itemList.Length;

            List<string> row = new List<string>();
            row.Add("x");
            row.Add("y");
            row.Add("name");
            GameManager.spawnData.Add(row);

        
            for (int i = len_enemy - 1; i >= 0; i--)
            {
                List<string> temp = new List<string>();
                temp.Add(enemyList[i].transform.position.x.ToString());
                temp.Add(enemyList[i].transform.position.y.ToString());
                temp.Add(enemyList[i].name[6].ToString());
                GameManager.spawnData.Add(temp);
            }

            for (int i = len_item - 1; i >= 0; i--)
            {
                List<string> temp = new List<string>();
                temp.Add(itemList[i].transform.position.x.ToString());
                temp.Add(itemList[i].transform.position.y.ToString());
                temp.Add("10"); // 아이템은 10번
                GameManager.spawnData.Add(temp);
            }

            CSVWriter.Write(GameManager.spawnData, "SpawnData");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
