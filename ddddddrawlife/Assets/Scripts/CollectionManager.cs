using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager CM;
    public CollectionData collectiondata = new CollectionData();
    public List<GameObject> objectList = new List<GameObject>();
    public int jobNum = 8; // 직업 개수 

    public string[] jobs = new string[8] { "의사", "판사", "개발자", "회사원", "스트리머", "화가", "운동선수", "용사" };



    void Awake() {
        CM = this;
    }

    void Start() {
        LoadCollection(); // 도감 정보 불러오기

        for (int i = 0; i < jobNum; i++){
            if(collectiondata.collectionList[i]==0){ // 획득한 적 없는 직업
                objectList[i].GetComponentInChildren<Text>().text = "?";

                objectList[i].GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
            }
            else if(collectiondata.collectionList[i]==1){ // 배드엔딩
                objectList[i].GetComponentInChildren<Text>().text = jobs[i];
                objectList[i].GetComponent<Image>().color = new Color(0, 0, 0, 0.2f);
            }
            else if(collectiondata.collectionList[i]==2){ // 노말엔딩
                objectList[i].GetComponentInChildren<Text>().text = jobs[i];
                objectList[i].GetComponent<Image>().color = new Color(255, 255, 255, 1);
            }
            else if(collectiondata.collectionList[i]==3){ // 해피엔딩을 본 경우
                objectList[i].GetComponentInChildren<Text>().text = jobs[i];
                objectList[i].GetComponent<Image>().color = new Color(207/255f, 255/255f, 182/255f, 1);
            }
        }
    }

    public void SaveCollection(){ // 저장
        string data = JsonConvert.SerializeObject(collectiondata);
        File.WriteAllText(Application.persistentDataPath + "/data.json", data);
        // for (int i = 0; i<8; i++){
        //     Debug.Log(collectiondata.collectionList[i]);
        // }
    }

    public void LoadCollection(){ // 불러오기 
        string strFile = Application.persistentDataPath + "/data.json";
        FileInfo fileInfo = new FileInfo(strFile);
        if(fileInfo.Exists){ // 세이브 파일이 있는 경우 
            string data = File.ReadAllText(Application.persistentDataPath + "/data.json");
            CollectionData temp = JsonConvert.DeserializeObject<CollectionData>(data);
            collectiondata.collectionList = temp.collectionList;
        }
        else{ // 없는 경우 
            for (int i = 0; i<8; i++){
                collectiondata.collectionList[i] = 0;
            }
            SaveCollection();
        }
        
    }
}

[System.Serializable]

public class CollectionData
{
    public List<int> collectionList = new List<int>(); // 직업 획득 여부 리스트
}
