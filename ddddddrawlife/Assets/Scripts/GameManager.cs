using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public static List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    public static List<List<string>> spawnData = new List<List<string>>();
    public static List<Dictionary<string, object>> spawnDataRead = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> spawnData2Read = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> spawnData3Read = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> spawnData4Read = new List<Dictionary<string, object>>();



    //public AgeInfo infancy = new AgeInfo(6, 4.0f);
    //public AgeInfo child = new AgeInfo(6, 4.0f);
    //public AgeInfo student = new AgeInfo(6, 4.0f);
    //public AgeInfo adult = new AgeInfo(6, 4.0f);

    public AgeInfo infancy = new AgeInfo(7, 32.0f);
    public AgeInfo child = new AgeInfo(6, 40.0f);
    public AgeInfo student = new AgeInfo(6, 80.0f);
    public AgeInfo adult = new AgeInfo(6, 94.0f);

    public string Name;//이름
    public bool Ismale;//남자인지 여부
    public int age = 0; // 나이
    public bool stage_end = false;


    public int nstate = 0; // 0: 유아기, 1: 유년기, 2: 학생기, 3: 성인기
    public string[] child_items; // 유년기 아이템 이름 배열
    public string[] jobs; //직업 가짓수 배열
    public string[,] job_category; // 직업 카테고리 for quiz
    public GameObject CorrectAnswer; // 정답 직업 프리펩
    public GameObject WrongAnswer; // 오답 직업 프리펩
    public bool ShowAnswer; //퀴즈를 풀었는지 안풀었는지 여부
    public bool Correct; //맞췄는지 여부
    public int dif; // 오답인 카테고리
    public string njob; // 현재 직업
    public int jobIdx; // 직업의 인덱스 
    public Gauge2Info ncharacter; // 현재 캐릭터 수치 정보
    public float decrease_speed = 0.5f; // 감정게이지 감소 속도

    public GameObject player; // 플레이어
    public GameObject emotion_gaugeBar; // 감정 게이지
    Image egauge, agauge;
    public GameObject achieve_gaugeBar; // 성취도 게이지
    public GameObject ending_g; // good ending
    public GameObject ending_s; // soso ending
    public GameObject ending_b; // bad ending
    public GameObject ending_f; // fail ending
    public GameObject ending_t; // ending text
    public GameObject ShowAge; // 나이 표시 텍스트
    public GameObject goal1; // 목표 성취도 표시 
    public GameObject goal2;


    public GameObject LoadingCanvas; //스테이지 넘어갈때 화면 검게 해줄 캔버스
    public GameObject BlackScreen; //로딩캔버스내에 있는 검은 이미지
    public GameObject Go123; //3,2,1숫자를 센후 시작
    public GameObject StartText; // 게임시작시 Start!글자
    public GameObject QuizCanvas; // 직업 퀴즈 캔버스
    public Text[] QuizBtnText; // 2지선다 버튼 텍스트
    public int CorF; // 퀴즈 정답 여부(0: 선택 안함, 1: 정답, 2: 오답)
    public Text QuizText; // 퀴즈 텍스트
    public GameObject[] hurdles; //장애물 배열
    public GameObject[] Items_stage1; // 유아기 아이템 배열
    public GameObject[] Items_stage2; // 유년기 아이템 배열
    public Sprite[] Items_stage3; // 학생기 아이템 배열
    public GameObject[] Arrow_items; // 화살표 아이템 배열 
    public GameObject[] realItems_stage2; // 유년기 때 나오는 아이템 배열
    public List<Stage3> realItems_stag3 = new List<Stage3>(); // 학생기 때 나오는 아이템 배열
    public GameObject Item_adult;
    public GameObject EmotionItem; // 감정 아이템
    public Sprite[] arrow_sprite; // 화살표 이미지들
    public List<GameObject> minigame_sprite = new List<GameObject>(); // 미니게임 창에 나오는 이미지들
    public List<int> minigame_arrow_list = new List<int>(); // 미니게임 창에 나온 화살표 번호 배열 
    public int minigame_arrrow_index = 0; // 클릭된 화살표와 비교해야하는 화살표의 인덱스 
    public int clicked_arrow; // 클릭된 화살표 번호 
    public bool minigameFailed = false;
    public bool minigameEnd = false;
    public GameObject minigameCanvas;
    public GameObject minigameMessage;
    public GameObject successMessage;
    public GameObject failMessage;
    public GameObject minigameTimeText;
    public int minigametime = 5;
    public GameObject minigameFlag;
    public GameObject pauseUI; // 일시정지 화면

    public bool isPause = false; // 일시정지
    public GameObject pauseButton; // 버튼
    public Sprite pauseSprite; // 일시정지 이미지
    public Sprite playSprite; // 플레이 이미지
    public GameObject pauseMessage; // 일시정지 글자 

    public GameObject tutorialCanvas; // 설명 캔버스
    public string player_name; // 사용자 이름
    public GameObject jumpButton;
    public GameObject slideButton;

    public GameObject spawnEnemy; // 장애물 생성 오브젝트 
    public Camera main_cam; // 카메라

    public Sprite floor_1; // 바닥 (유아기)
    public Sprite floor_2; // (유년기)
    public Sprite floor_3; // (학생기)
    public Sprite floor_4; // (성인기)
    public Sprite player_1; // 플레이어 (유아기)
    public Sprite player_2_male; // 유년기(남)
    public Sprite player_2_female; // 유년기(여)
    public Sprite player_3_male; // 학생기(남)
    public Sprite player_3_female; // 학생기(여)
    //public GameObject[] colliders; // 각 스테이지의 플레이어 콜라이더
    public SpriteRenderer[] groundSprites;
    public GameObject[] BGObjects;
    public Sprite BG_1; // 배경(유아기)
    public Sprite BG_2;
    public Sprite BG_3;
    public Sprite BG_4;

    public GameObject progress_gaugeBar; // 스테이지 진행도 게이지
    Image progress_gauge;
    public float timer;


    //public Sprite MaleImage; //남자 이미지
    //public Sprite FemaleImage; //여자 이미지


    // 애니메이션들
    public RuntimeAnimatorController baby_ani;
    public RuntimeAnimatorController bchild_ani;
    public RuntimeAnimatorController gchild_ani;
    public RuntimeAnimatorController bstudent_ani;
    public RuntimeAnimatorController gstudent_ani;
    public RuntimeAnimatorController man_ani;
    public RuntimeAnimatorController woman_ani;

    private void Awake()
    {
        GM = this;
        data = CSVReader.Read("AgaugeInfo");
        spawnDataRead = CSVReader.Read("SpawnData"); // 유아기 스폰 데이터
        spawnData2Read = CSVReader.Read("SpawnData2"); // 유년기 스폰 데이터
        spawnData3Read = CSVReader.Read("SpawnData3"); // 학생기 스폰 데이터
        spawnData4Read = CSVReader.Read("SpawnData4"); // 성인기 스폰 데이터
        Ismale = System.Convert.ToBoolean(PlayerPrefs.GetInt("IsMale")); //성별 불러와 저장

        // 유아기 캐릭터
        //player.GetComponent<SpriteRenderer>().sprite = player_1;

        //여자인지 남자인지에 따라 다른 애니메이션(유아기 캐릭터 추가 시 변할 필요 있음.)
        // if (Ismale)
        // {
        //     player.GetComponent<SpriteRenderer>().sprite = player_3_male;
        //     player_ani.SetBool("IsMale", true);
        // }
        // else
        // {
        //     player.GetComponent<SpriteRenderer>().sprite = player_3_female;
        //     player_ani.SetBool("IsMale", false);
        // }

        child_items = new string[6] { "창의력", "논리력", "암기력", "지구력", "집중력", "주도력" };
        jobs = new string[8] { "의사", "판사", "개발자", "회사원", "스트리머", "화가", "운동선수", "용사" };
        job_category = new string[,] { { "의사", "판사", "개발자", "회사원" }, { "스트리머", "화가", "운동선수", "화가" } };
        jobIdx = -1;
    }

    void Start()
    {
        main_cam.transform.position = new Vector3(0, 32, -10);
        Time.timeScale = 0;
        ShowAnswer = false;
        Correct = false;
        progress_gauge = progress_gaugeBar.GetComponent<Image>();

        // 이름 불러와 저장
        if (PlayerPrefs.HasKey("PName")){
            if(PlayerPrefs.GetString("PName") == "")
                player_name = "아무개";
            else
                player_name = PlayerPrefs.GetString("PName");    
        }
        else
            player_name = "아무개";

        // 유년기 아이템
        realItems_stage2 = new GameObject[4];

        StartCoroutine(LifeCycle());
        egauge = emotion_gaugeBar.GetComponent<Image>();
        agauge = achieve_gaugeBar.GetComponent<Image>();

        // 바닥 이미지 변경

        groundSprites[0].sprite = floor_1;
        groundSprites[1].sprite = floor_1;
        groundSprites[2].sprite = floor_1;

        // 플레이어 애니메이션 변경(유아기)
        player.GetComponent<Animator>().runtimeAnimatorController = baby_ani;

 
    }

   void Update()
    {
        timer += Time.deltaTime;
    }

    void RandomItemChoice(GameObject[] stage) // + 아이템 2개 - 아이템 2개 뽑기
    {
        int rn3, rn4;
        List<int> plusItem = new List<int>();
        List<int> minusItem = new List<int>();

        // + 아이템, - 아이템 분류
        for (int i = 0; i < stage.Length; i++)
        {
            if (stage[i].tag == "ITEMPLUS")
            {
                plusItem.Add(i);
            }
            else
            {
                minusItem.Add(i);
            }
        }

        // real 배열에 넣기
        realItems_stage2[0] = stage[CutsceneManager.CM.item2_idx[0]];
        realItems_stage2[1] = stage[CutsceneManager.CM.item2_idx[1]];

        rn3 = Random.Range(0, minusItem.Count);
        realItems_stage2[2] = stage[minusItem[rn3]];
        rn4 = Random.Range(0, minusItem.Count);
        while (rn3 == rn4)
        {
            rn4 = Random.Range(0, minusItem.Count);
        }
        realItems_stage2[3] = stage[minusItem[rn4]];
    }

    IEnumerator StageIntervalRun() // 한 스테이지 끝나고 달려나가는..
    {
        yield return new WaitForSeconds(2f); //2초뒤에
        while (player.transform.position.x < 10)
        {
            player.transform.position += new Vector3(1, 0, 0);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator AgePlus(AgeInfo a) // 나이 및 시간 증가
    {
        for (int i = 0; i < a.period; i++)
        {
            age++;
            ShowAge.GetComponent<Text>().text = "" + age + "살";

            yield return new WaitForSeconds(a.time / a.period);
        }
    }

    IEnumerator Stage_Progress() // 스테이지 진행도
    {
        timer = 0;

        while (!stage_end)
        {
            switch (nstate)
            {
                case 0:
                    progress_gauge.fillAmount = timer / infancy.time;
                    break;
                case 1:
                    progress_gauge.fillAmount = timer / child.time;
                    break;
                case 2:
                    progress_gauge.fillAmount = timer / student.time;
                    break;
                case 3:
                    progress_gauge.fillAmount = timer / adult.time;
                    break;

            }

            yield return new WaitForSeconds(0.1f);
        }
        
    }


    IEnumerator Decrease_emotion_gauge()
    { // 감정게이지 지속적으로 감소(학생기때부터 적용)
        Gauge gauge = GameObject.Find("Player").GetComponent<Gauge>();

        while (GameManager.GM.nstate >= 2 && gauge.emotion_gauge > 0 && !stage_end)
        {
            if (GameManager.GM.nstate == 2)
                gauge.emotion_gauge -= 1;
            if (GameManager.GM.nstate == 3)
                gauge.emotion_gauge -= 1.5f;
            yield return new WaitForSeconds(decrease_speed);
        }
    }

    IEnumerator Minigame_Delay() // 미니게임 끝나고 3초 딜레이 
    {
        yield return new WaitForSeconds(3);
        minigameCanvas.SetActive(false);
    }


    public void start_minigame() // 미니게임 시작 
    {
        minigameCanvas.SetActive(true);
        minigame_arrow_list.Clear();


        // 캔버스에 화살표 이미지 추가 
        // 화살표 배열 만들기 (왼, 아래, 오른, 위, 왼)
        minigame_arrow_list.Add(0);
        minigame_sprite[0].GetComponent<Image>().sprite = arrow_sprite[0];

        minigame_arrow_list.Add(1);
        minigame_sprite[1].GetComponent<Image>().sprite = arrow_sprite[1];

        minigame_arrow_list.Add(2);
        minigame_sprite[2].GetComponent<Image>().sprite = arrow_sprite[2];

        minigame_arrow_list.Add(3);
        minigame_sprite[3].GetComponent<Image>().sprite = arrow_sprite[3];

        minigame_arrow_list.Add(0);
        minigame_sprite[4].GetComponent<Image>().sprite = arrow_sprite[0];

        for (int i = 0; i < 5; i++)
        {
            minigame_sprite[i].GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        }

    }

    public void minigame_check(int arrow_num)
    { // 순서에 맞게 먹었는지 체크 
        Debug.Log(arrow_num);

        if (minigame_arrow_list[minigame_arrrow_index] == arrow_num)
        { // 맞게 먹음
            Debug.Log("맞");
            minigame_sprite[minigame_arrrow_index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            minigame_arrrow_index++;

            if (minigame_arrrow_index == 5)
            {
                minigameMessage.SetActive(false);
                successMessage.SetActive(true); // 성공 메세지
                minigameEnd = true; // 미니게임 끝
                minigameFailed = false;
                Gauge gauge = GameObject.Find("Player").GetComponent<Gauge>(); // 성취도 게이지 가져오기 
                if (gauge.achieve_gauge >= 80) gauge.achieve_gauge = 100;
                else gauge.achieve_gauge += 20;

                //if(gauge.emotion_gauge>=90)gauge.emotion_gauge = 100;
                //else gauge.emotion_gauge += 10;

                StartCoroutine(Minigame_Delay());

            }
        }
        else
        { // 틀리게 입력됨 
            Debug.Log("틀");
            minigameEnd = true; // 미니게임 끝
            minigameFailed = true; // 미니게임 실패 
            minigameMessage.SetActive(false);
            failMessage.SetActive(true); // 실패 메세지  
            Gauge gauge = GameObject.Find("Player").GetComponent<Gauge>();
            if (gauge.achieve_gauge < 20) gauge.achieve_gauge = 0;
            else gauge.achieve_gauge -= 20;

            StartCoroutine(Minigame_Delay());

        }
    }

    IEnumerator StageInterval() // 스테이지 사이에 일어나는 일
    {
        stage_end = true; // 한 스테이지 끝 
        if (nstate == 2) // 학생기 끝나고 장애물 형식 퀴즈
        {
            int answer_num = Random.Range(0, 2); // 0 or 1
            dif = 0; // 오답의 카테고리
            for (int l = 0; l < 4; l++)
            {
                if (job_category[0, l] == njob) dif = 1;
            }
            if (answer_num == 0)
            {
                Instantiate(CorrectAnswer, new Vector3(11, 1, 0), new Quaternion(0, 0, 0, 0));
                Instantiate(WrongAnswer, new Vector3(11, -1.6f, 0), new Quaternion(0, 0, 0, 0));
            }
            else
            {
                Instantiate(CorrectAnswer, new Vector3(11, -1.6f, 0), new Quaternion(0, 0, 0, 0));
                Instantiate(WrongAnswer, new Vector3(11, 1, 0), new Quaternion(0, 0, 0, 0));
            }
            yield return new WaitUntil(() => ShowAnswer); // 답을 제출할때까지(부딪힐때까지) 대기
            if (Correct == false)
            { // 오답
                ending_t.SetActive(true);
                ending_f.SetActive(true);
                ending_t.GetComponent<Text>().text = "저런... " + player_name + "의 운명을 찾지 못하였구나";
            }
            else
            { // 정답
                QuizCanvas.SetActive(true);
                QuizText.text = "훌륭하구나.. 정답이란다..";
            }
            yield return new WaitForSecondsRealtime(2f);
            QuizCanvas.SetActive(false);
        }
        yield return StartCoroutine(StageIntervalRun());
        Time.timeScale = 0; //->로딩시작
        LoadingCanvas.SetActive(true); //UI활성화 후
        BlackScreen.SetActive(true); //화면 검게 만듬

        nstate++;

        switch (nstate)
        {
            case 1: // 유년기 바닥, 배경, 캐릭터 애니메이션
                groundSprites[0].sprite = floor_2;
                groundSprites[1].sprite = floor_2;
                groundSprites[2].sprite = floor_2;
                BGObjects[0].GetComponent<SpriteRenderer>().sprite = BG_2;
                BGObjects[1].GetComponent<SpriteRenderer>().sprite = BG_2;
                BGObjects[0].gameObject.transform.localPosition = new Vector2(1.6f, 1.6f);
                BGObjects[1].gameObject.transform.localPosition = new Vector2(37.6f, 1.6f);
                goal1.SetActive(true); // 목표 성취도 표시 

                if (Ismale) player.GetComponent<Animator>().runtimeAnimatorController = bchild_ani;
                else player.GetComponent<Animator>().runtimeAnimatorController = gchild_ani;
                player.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                break;
            case 2: // 학생기 바닥, 배경, 캐릭터 애니메이션
                groundSprites[0].sprite = floor_3;
                groundSprites[1].sprite = floor_3;
                groundSprites[2].sprite = floor_3;
                BGObjects[0].GetComponent<SpriteRenderer>().sprite = BG_3;
                BGObjects[1].GetComponent<SpriteRenderer>().sprite = BG_3;
                BGObjects[0].gameObject.transform.localPosition = new Vector2(1.6f, 1.6f);
                BGObjects[1].gameObject.transform.localPosition = new Vector2(43f, 1.6f);
                goal1.SetActive(false);
                goal2.SetActive(true);  // 목표 성취도 표시 
                if (Ismale) player.GetComponent<Animator>().runtimeAnimatorController = bstudent_ani;
                else player.GetComponent<Animator>().runtimeAnimatorController = gstudent_ani;
                player.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.1f);
                break;
            case 3: // 성인기 바닥, 배경, 캐릭터 애니메이션
                groundSprites[0].sprite = floor_4;
                groundSprites[1].sprite = floor_4;
                groundSprites[2].sprite = floor_4;
                goal2.SetActive(false);
                BGObjects[0].GetComponent<SpriteRenderer>().sprite = BG_4;
                BGObjects[1].GetComponent<SpriteRenderer>().sprite = BG_4;
                BGObjects[0].gameObject.transform.localPosition = new Vector2(1.6f, 1.6f);
                BGObjects[1].gameObject.transform.localPosition = new Vector2(37.6f, 1.6f);
                if (Ismale) player.GetComponent<Animator>().runtimeAnimatorController = man_ani;
                else player.GetComponent<Animator>().runtimeAnimatorController = woman_ani;
                break;
        }

        progress_gauge.fillAmount = 0; // 진행도 초기화

        yield return new WaitForSecondsRealtime(2f); //2초뒤에
        player.transform.position = new Vector3(-6f, -1.87f, 0);
        Color Black = BlackScreen.GetComponent<Image>().color;//직접 getcomponent로는 수정이 안됨 일단 color저장
        Black.a = 0.5f; //투명도 50%
        BlackScreen.GetComponent<Image>().color = Black;//다시 이미지에 반영
        Go123.SetActive(true); //숫자를 화면에 띄움
        Go123.GetComponent<Text>().text = "3";
        yield return new WaitForSecondsRealtime(1f);
        Go123.GetComponent<Text>().text = "2";
        yield return new WaitForSecondsRealtime(1f);
        Go123.GetComponent<Text>().text = "1";
        yield return new WaitForSecondsRealtime(1f);
        Black.a = 1f;
        BlackScreen.GetComponent<Image>().color = Black;
        BlackScreen.SetActive(false);
        Go123.SetActive(false); // 검은화면과 숫자를 비활성화
        StartText.SetActive(true); // Start!라는 텍스트를 화면에 띄움
        Time.timeScale = 1; //게임 이어서 시작
        start_tutorial(); // 튜토리얼
        yield return new WaitForSeconds(2f);
        stage_end = false;
        yield return new WaitForSecondsRealtime(1f);//1초이후에
        StartText.SetActive(false);
        LoadingCanvas.SetActive(false); //Start텍스트와 캔버스를 비활성화
    }

    public void close_tutorial()
    { // 설명 창 끄기
        tutorialCanvas.SetActive(false);
        jumpButton.GetComponent<Button>().interactable = true; // 점프 버튼 활성화
        slideButton.GetComponent<Button>().interactable = true; // 슬라이드 버튼 활성화 
        pauseButton.GetComponent<Button>().interactable = true; // 일시정지 버튼 활성화 
        Time.timeScale = 1;
    }

    public void start_tutorial()
    {
        if (!TutorialControl.skip_tutorial)
        { // 설명 함 
            Time.timeScale = 0;
            tutorialCanvas.SetActive(true);
            tutorialCanvas.transform.GetChild(nstate+1).gameObject.SetActive(true); // 각 스테이지마다 설명
            jumpButton.GetComponent<Button>().interactable = false; // 점프 버튼 비활성화
            slideButton.GetComponent<Button>().interactable = false; // 슬라이드 버튼 비활성화 
            pauseButton.GetComponent<Button>().interactable = false; // 일시정지 버튼 비활성화 

        }
        else
        {
            tutorialCanvas.SetActive(false);
        }
    }

    IEnumerator LifeCycle() // 중간 분기 및 엔딩 관리
    {
        //Debug.Log(Time.timeScale);
        yield return new WaitUntil(() => Time.timeScale == 1);
        Debug.Log(njob);
        //Debug.Log(ncharacter.childGauge[0]);
        //Debug.Log(ncharacter.studentGauge[0]);

        RandomItemChoice(Items_stage2); // 유년기 아이템 뽑기
        // 학생기 장애물 뽑기
        int rn1, rn2, rn3, rn4;
        List<int> plusItem = new List<int>();
        List<int> minusItem = new List<int>();

        // + 아이템, - 아이템 분류
        for (int i = 0; i < ncharacter.studentGauge.Count; i++)
        {
            if (ncharacter.studentGauge[i] > 0)
            {
                plusItem.Add(i);
            }
            else
            {
                minusItem.Add(i);
            }
        }

        // real 배열에 넣기
        rn1 = Random.Range(0, plusItem.Count);
        realItems_stag3.Add(new Stage3(Items_stage3[plusItem[rn1]], ncharacter.studentGauge[plusItem[rn1]]));
        rn2 = Random.Range(0, plusItem.Count);
        while (rn1 == rn2)
        {
            rn2 = Random.Range(0, plusItem.Count);
        }
        realItems_stag3.Add(new Stage3(Items_stage3[plusItem[rn2]], ncharacter.studentGauge[plusItem[rn2]]));
        rn3 = Random.Range(0, minusItem.Count);
        realItems_stag3.Add(new Stage3(Items_stage3[minusItem[rn3]], ncharacter.studentGauge[minusItem[rn3]]));
        rn4 = Random.Range(0, minusItem.Count);
        while (rn3 == rn4)
        {
            rn4 = Random.Range(0, minusItem.Count);
        }
        realItems_stag3.Add(new Stage3(Items_stage3[minusItem[rn4]], ncharacter.studentGauge[minusItem[rn4]]));

        // 성인기 콜라이더 끄고 유아기 콜라이더 켜기
        //colliders[3].SetActive(false);
        //colliders[nstate].SetActive(true);
        // 유아기
        Debug.Log("유아기");
        StartCoroutine(Stage_Progress());
        spawnEnemy.GetComponent<SpawnEnemy>().Spawn();
        start_tutorial();


        yield return StartCoroutine(AgePlus(infancy));


        yield return StartCoroutine(StageInterval());

        // 유년기
        Debug.Log("유년기");
        StartCoroutine(Stage_Progress());
        spawnEnemy.GetComponent<SpawnEnemy>().Spawn();

        yield return StartCoroutine(AgePlus(child));

        // 유년기 ~ 학생기 탈락 결정
        float fa = agauge.fillAmount;
        // if(fa < 0.2){
        //    Time.timeScale = 0;
        //    ending_t.SetActive(true);
        //    ending_f.SetActive(true);
        //    ending_t.GetComponent<Text>().text = player_name + "은(는) 학생이 되지 못했다!";
        // }

        // 유년기 콜라이더 끄고 학생기 콜라이더 켜기
        //colliders[nstate].SetActive(false);
        //nstate++;
        //colliders[nstate].SetActive(true);

        yield return StartCoroutine(StageInterval());



        // 학생기
        Debug.Log("학생기");
        StartCoroutine(Stage_Progress());
        spawnEnemy.GetComponent<SpawnEnemy>().Spawn();
        StartCoroutine(Decrease_emotion_gauge());


        yield return StartCoroutine(AgePlus(student));

        // 학생기 ~ 성인 탈락 결정
        fa = agauge.fillAmount;
        //if(fa < 0.5){
        //    Time.timeScale = 0;
        //    ending_t.SetActive(true);
        //    ending_f.SetActive(true);
        //    ending_t.GetComponent<Text>().text = player_name + "은(는) 어른이 되지 못했다!";
        //}


        // 직업 맞추는 퀴즈

        yield return StartCoroutine(StageInterval());

        // 성인기
        Debug.Log("성인기");
        StartCoroutine(Stage_Progress());
        spawnEnemy.GetComponent<SpawnEnemy>().Spawn();
        StartCoroutine(Decrease_emotion_gauge());


        yield return StartCoroutine(AgePlus(adult));

        yield return StartCoroutine(StageIntervalRun());


        Time.timeScale = 0;
        //엔딩
        Debug.Log("엔딩");
        fa = agauge.fillAmount;
        ending_t.SetActive(true);
        Text t = ending_t.GetComponent<Text>();
        if (fa <= 0.6f)
        {
            ending_b.SetActive(true);
            t.text = player_name + "은(는) 쩌리 인생을 살았다!"; // 좀 더 좋은 워딩이 있으면.. 바꿔주시면...
            if (CollectionManager.CM.collectiondata.collectionList[jobIdx] < 1)
            {
                CollectionManager.CM.collectiondata.collectionList[jobIdx] = 1; // 배드엔딩 
                CollectionManager.CM.SaveCollection();
            }

        }
        else if (fa <= 0.8f)
        {
            ending_s.SetActive(true);
            t.text = player_name + "은(는) 그럭저럭한 인생을 살았다!";
            if (CollectionManager.CM.collectiondata.collectionList[jobIdx] < 2)
            {
                CollectionManager.CM.collectiondata.collectionList[jobIdx] = 2; // 노말엔딩
                CollectionManager.CM.SaveCollection();
            }


        }
        else
        {
            ending_g.SetActive(true);
            t.text = player_name + "은(는) 완벽한 인생을 살았다!";
            if (CollectionManager.CM.collectiondata.collectionList[jobIdx] < 3)
            {
                CollectionManager.CM.collectiondata.collectionList[jobIdx] = 3; // 해피엔딩
                CollectionManager.CM.SaveCollection();
            }

        }

    }

    public void pause()
    {// 일시정지
        if (!isPause)
        {
            pauseUI.SetActive(true);
            pauseButton.GetComponent<Button>().interactable = false;
            //pauseButton.GetComponent<Image>().sprite = playSprite; // 이미지 변경
            jumpButton.GetComponent<Button>().interactable = false; // 점프 버튼 비활성화
            slideButton.GetComponent<Button>().interactable = false; // 슬라이드 버튼 비활성화 
            pauseMessage.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
        }
        //else
        //{ // 계속하기 누르면 시작 
        //    pauseButton.GetComponent<Image>().sprite = pauseSprite;
        //    jumpButton.GetComponent<Button>().interactable = true;
        //    slideButton.GetComponent<Button>().interactable = true;
        //    pauseMessage.SetActive(false);
        //    Time.timeScale = 1;
        //    isPause = false;
        //}
    }

    public void continue_game() // 계속하기
    {
        if (isPause)
        {
            pauseButton.GetComponent<Button>().interactable = true;
            jumpButton.GetComponent<Button>().interactable = true;
            slideButton.GetComponent<Button>().interactable = true;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
        }
        
    }

}

// 각 스테이지 길이에 대한 정보를 담는 class
public class AgeInfo
{
    public int period; // 각 스테이지 별 나이
    public float time; // 각 스테이지 별 시간
    public AgeInfo(int p, float t)
    {
        period = p;
        time = t;
    }
}

public class Stage3
{
    public Sprite img;
    public int state;
    public Stage3(Sprite i, int s)
    {
        img = i;
        state = s;
    }
}

[System.Serializable]
// 각 직업에 대한 수치 정보를 담는 class
public class Gauge2Info
{
    // 0: 창의력, 1: 논리력, 2: 암기력, 3: 지구력, 4: 집중력, 5: 주도력
    public List<int> childGauge = new List<int>();

    // 0: 예술, 1: 이과 공부, 2: 문과 공부, 3: 컴퓨터, 4: 운동, 5: 발표, 6: 사회성
    public List<int> studentGauge = new List<int>();
    public string job;
    public bool get; // 이 직업 해금 여부
    public Gauge2Info(string inputjob)
    {
        job = inputjob;
        get = false;
        for (int i = 0; i <= 5; i++)
        {
            childGauge.Add(int.Parse(GameManager.data[i][inputjob].ToString()));
        }
        for (int i = 6; i < 13; i++)
        {
            studentGauge.Add(int.Parse(GameManager.data[i][inputjob].ToString()));
        }
    }
}



