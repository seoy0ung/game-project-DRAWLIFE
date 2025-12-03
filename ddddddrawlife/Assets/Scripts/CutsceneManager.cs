using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager CM;
    public static List<Dictionary<string, object>> jobselect_data = new List<Dictionary<string, object>>();
    public Camera main_cam;
    public Text maria_talk;
    public float talk_speed = 0.15f;
    public float fall_speed = 0.15f;
    private string what_to_talk;
    List<string> text_list = new List<string>();
    public GameObject[] power_choice;
    public GameObject transparent;
    public int text_index;
    public bool text_end;
    public bool select=false;

    public GameObject baby;
    public GameObject background;
    public List<int> item2_idx; // 선택한 유년기 아이템 인덱스 저장
    private Color[] btn_color;

    // play scene gameObject
    public GameObject gauge_canvas; // 게이지바 캔버스
    public GameObject btn_canvas; // 버튼 캔버스
    public GameObject player; // 플레이어

    public AudioSource bgm;
    public AudioClip bgmClip;
    private void Awake() {        
        CM = this;
        btn_color = new Color[]{Color.red, new Color(1f, 127/255f, 0), Color.yellow, Color.green, Color.blue, new Color(139/255f, 0, 1f)};
        jobselect_data = CSVReader.Read("JobSelect");
        text_list.Add("어서오렴  새  아가야.");
        text_list.Add("너는  어떤  능력을  가지고  있니?\n\n두  개를  선택해주렴.");
        text_list.Add("저런,  그  조합은  아직  준비되지  않은  것  같구나...\n다시  선택해주렴.");
        text_list.Add("좋은  능력을  가지고  있구나!");
        text_list.Add("너는...이  될  운명이구나!");
        text_list.Add("부디  운명을  따라  좋은  인생을\n\n살기  바란다.");


    }

    private void Start(){
        item2_idx = new List<int>();
        text_index = 0;
    }

    private void OnEnable() {
        what_to_talk = text_list[0];
            //+"\n\n"+text_list[1];
        StartCoroutine(Talking());
    }

    public void next_text()
    {
        if (text_end) // 대사가 다 나옴 
        {
            if (text_index == 1 && !select) // 아직 버튼 선택 안함
            {
                return;
            }
            else if (text_index == 2 && !select) return; // 잘못 선택했음 
            else if (text_index>=2 && select && text_index!=5)
            {
                text_end = false;
                maria_talk.text = "";
                text_index++;
                what_to_talk = text_list[text_index];
                StartCoroutine(Talking());
            }
            else if (text_index ==5)
            {
                maria_talk.text = "";
                Debug.Log("!!!!!!!!!!");
                StartCoroutine(mariaSceneEnd());
                
            }
            else
            {
    
                text_end = false;
                maria_talk.text = "";
                text_index++;
                what_to_talk = text_list[text_index];
                StartCoroutine(Talking());
            }
            
        }
        else // 대사가 다 안나왔을때 터치
        {

            maria_talk.text = "";
            maria_talk.text = text_list[text_index]; // 모든 텍스트 출력 
            text_end = true; // 텍스트 끝 
            if (text_index == 1) // 이 대사 후에 버튼 나와야함
            {
                foreach (GameObject btn in power_choice)
                {
                    
                    btn.SetActive(true);
                    
                }
                StartCoroutine(wait_select());

            }

        }
 

    }

    IEnumerator mariaSceneEnd()
    {
        background.GetComponent<BoxCollider2D>().enabled = false;
        while (main_cam.transform.position.y > 0)
        {
            baby.transform.Rotate(new Vector3(0, 0, 50f), 10f);
            baby.transform.position -= new Vector3(0.08f, 0.93f, 0);
            main_cam.transform.Translate(new Vector2(0, -1f));
            baby.transform.localScale -= new Vector3(0.023f, 0.023f, 0.023f);
            yield return new WaitForSecondsRealtime(fall_speed);
        }

        // 씬 끝 ~ play씬 gameobject들 활성화
        gauge_canvas.SetActive(true);
        btn_canvas.SetActive(true);
        player.SetActive(true);
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        for (int i = 0; i <= 3; i++)
        {
            baby.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.25f);
            player.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.25f);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        transparent.SetActive(false);
        Time.timeScale = 1;
        bgm.clip = bgmClip;
        bgm.Play();
        Debug.Log(GameManager.GM.jobIdx);
    }

    IEnumerator Talking()
    {
        maria_talk.text = "";
        Debug.Log(text_index);
        for (int i = 0; i < what_to_talk.Length; i++)
        {
            if (text_end)
            {
                break;
            }
            maria_talk.text += what_to_talk[i];
            
            yield return new WaitForSecondsRealtime(talk_speed);
        }
        yield return null;
    }

    IEnumerator wait_select() // 버튼 선택
    {
        while (GameManager.GM.jobIdx == -1)
        {
            item2_idx.Clear();

            // 두 개 선택할 때까지 기다리기
            yield return new WaitUntil(() => item2_idx.Count == 2);
            GameManager.GM.jobIdx = -1;
            for (int j = 0; j < jobselect_data.Count; j++)
            {
                if (int.Parse(jobselect_data[j][GameManager.GM.child_items[item2_idx[0]]].ToString()) == 1 && int.Parse(jobselect_data[j][GameManager.GM.child_items[item2_idx[1]]].ToString()) == 1)
                    GameManager.GM.jobIdx = int.Parse(jobselect_data[j]["직업"].ToString());
            }
            if (GameManager.GM.jobIdx == -1) // 없는 조합 
            {
                text_end = false;
                text_index = 2;
                what_to_talk = text_list[text_index];
                maria_talk.text = "";
                StartCoroutine(Talking());

                power_choice[item2_idx[0]].GetComponent<Image>().color = new Color(1, 1, 1);
                power_choice[item2_idx[1]].GetComponent<Image>().color = new Color(1, 1, 1);
            }
        }

        
        // 직업 결정
        GameManager.GM.njob = GameManager.GM.jobs[GameManager.GM.jobIdx];
        GameManager.GM.ncharacter = new Gauge2Info(GameManager.GM.njob);
        for (int i = 0; i < GameManager.GM.ncharacter.childGauge.Count; i++) GameManager.GM.Items_stage2[i].GetComponent<ItemSetting>().CSetting(i);


        // 선택 후 버튼 처리
        transparent.SetActive(true);
        foreach (int i in item2_idx)
        {
            power_choice[i].GetComponent<Image>().color = btn_color[i];
        }
        yield return new WaitForSecondsRealtime(1f);
        foreach (GameObject btn in power_choice)
        {
            btn.SetActive(false);
        }

        text_index = 3;
        what_to_talk = text_list[3];
        select = true;
        text_end = false;
        StartCoroutine(Talking());
    }

    // 마리아씬 관장 코루틴
    IEnumerator MariaTalking()
    {
        // 말하는 느낌을 주고 싶어 한글자씩 나오게 하는
        for(int i = 0; i < what_to_talk.Length ; i++){
            maria_talk.text += what_to_talk[i];
            //if(what_to_talk[i] == ' ') maria_talk.text += " "; // 글자 간격이 좀 좁아서 두번 스페이스바 되게
            if (text_end) break;
            yield return new WaitForSecondsRealtime(talk_speed);
        }
            // 유년기 ~력 선택하는 버튼 팝업
        foreach(GameObject btn in power_choice)
        {
            btn.SetActive(true);
        }

        while(GameManager.GM.jobIdx == -1){
            item2_idx.Clear();
            
            // 두 개 선택할 때까지 기다리기
            yield return new WaitUntil(() => item2_idx.Count == 2);
            GameManager.GM.jobIdx = -1;
            for(int j = 0; j < jobselect_data.Count; j++){
                if(int.Parse(jobselect_data[j][GameManager.GM.child_items[item2_idx[0]]].ToString()) == 1 && int.Parse(jobselect_data[j][GameManager.GM.child_items[item2_idx[1]]].ToString()) == 1)
                    GameManager.GM.jobIdx = int.Parse(jobselect_data[j]["직업"].ToString());
            }
            if(GameManager.GM.jobIdx == -1){
                what_to_talk = text_list[2];
                maria_talk.text = "";
                for(int i = 0; i < what_to_talk.Length ; i++){
                    maria_talk.text += what_to_talk[i];
                    if(what_to_talk[i] == ' ') maria_talk.text += " "; // 글자 간격이 좀 좁아서 두번 스페이스바 되게
                    yield return new WaitForSecondsRealtime(talk_speed);
                }
            power_choice[item2_idx[0]].GetComponent<Image>().color = new Color(1,1,1);
            power_choice[item2_idx[1]].GetComponent<Image>().color = new Color(1,1,1);
            }
        }
        // 직업 결정
        GameManager.GM.njob = GameManager.GM.jobs[GameManager.GM.jobIdx];
        GameManager.GM.ncharacter = new Gauge2Info(GameManager.GM.njob);
        for (int i = 0; i < GameManager.GM.ncharacter.childGauge.Count; i++) GameManager.GM.Items_stage2[i].GetComponent<ItemSetting>().CSetting(i);
        

        // 선택 후 버튼 처리
        transparent.SetActive(true);
        foreach(int i in item2_idx){
            power_choice[i].GetComponent<Image>().color = btn_color[i];
        }
        yield return new WaitForSecondsRealtime(1f);
        foreach(GameObject btn in power_choice)
        {
            btn.SetActive(false);
        }

        // 씬 끝날 때 멘트
        maria_talk.text = "";
        what_to_talk = text_list[3];
        for(int i = 0; i < what_to_talk.Length ; i++){
            maria_talk.text += what_to_talk[i];
            if(what_to_talk[i] == ' ') maria_talk.text += " "; // 글자 간격이 좀 좁아서 두번 스페이스바 되게
            yield return new WaitForSecondsRealtime(talk_speed);
        }
        maria_talk.text = "";
        what_to_talk = text_list[4]+"\n\n"+text_list[5];
        for (int i = 0; i < what_to_talk.Length ; i++){
            maria_talk.text += what_to_talk[i];
            if(what_to_talk[i] == ' ') maria_talk.text += " "; // 글자 간격이 좀 좁아서 두번 스페이스바 되게
            yield return new WaitForSecondsRealtime(talk_speed);
        }
        maria_talk.text = "";
        while(main_cam.transform.position.y > 0){
            baby.transform.Rotate(new Vector3(0, 0, 50f), 10f);
            baby.transform.position -= new Vector3(0, 2, 0);
            main_cam.transform.Translate(new Vector2(0, -2));
            baby.transform.localScale -= new Vector3(0.125f, 0.125f, 0.125f);
            yield return new WaitForSecondsRealtime(fall_speed);
        }
        
        // 씬 끝 ~ play씬 gameobject들 활성화
        gauge_canvas.SetActive(true);
        btn_canvas.SetActive(true);
        player.SetActive(true);
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        for(int i = 0;i <= 3; i++){
            baby.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,0.25f);
            player.GetComponent<SpriteRenderer>().color += new Color(0,0,0,0.25f);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        transparent.SetActive(false);
        Time.timeScale = 1;
        Debug.Log(GameManager.GM.jobIdx);
    }
}
