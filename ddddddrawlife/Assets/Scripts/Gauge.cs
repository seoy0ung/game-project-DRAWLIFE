using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public float speed = 2;
    public float emotion_gauge = 50; // 감정 게이지(반 채워진 상태)
    public float max_emotion_gauge = 100;
    public float achieve_gauge = 0;
    public float max_achieve_gauge = 100;
    public GameObject emotion_gaugebar; // 감정 게이지
    public GameObject achieve_gaugebar; // 성취도 게이지
    Image egauge, agauge;
    public float decrease_speed = 0.5f; // 감정게이지 감소 속도
    public SpriteRenderer player_sprite;
    public float Emotion_gauge_percent; //감정게이지가 채워진 비율(50%가 평범한 상태)
    float change_value;
    public bool startMinigame = false;
    public GameObject emotionUI;
    public GameObject acheiveUI;
    public Animator anim;

    void Start()
    {
        //StartCoroutine(Decrease_emotion_gauge());
        player_sprite = GetComponent<SpriteRenderer>();
        egauge = emotion_gaugebar.GetComponent<Image>();
        agauge = achieve_gaugebar.GetComponent<Image>();

    }

    void Update()
    {
        //emotion_gaugebar.GetComponent<Image>().fillAmount = emotion_gauge / max_emotion_gauge; // 게이지 갱신
        agauge.fillAmount = achieve_gauge / max_achieve_gauge;
        egauge.fillAmount = emotion_gauge / max_emotion_gauge; // 게이지 갱신   
        Emotion_gauge_percent = emotion_gauge / 50f; //감정게이지가 최대로 되었을 때 성취게이지가 2배로 참

    }

    private void OnTriggerEnter2D(Collider2D other)
    { // 장애물 또는 아이템과 부딪혔을 때
        if (other.CompareTag("Enemy1"))
        { // 감정게이지 -
            emotion_gauge -= 6;
            StartCoroutine("Player_blink"); // 캐릭터 깜빡임 효과 
            // Destroy(other.gameObject); // 적 파괴
        }
        else if (other.CompareTag("Item_toy"))
        { // 감정게이지 +
            StartCoroutine(Emotion_Plus());
            if (emotion_gauge >= 98.5f)
            {
                emotion_gauge = max_emotion_gauge;
            }
            else
            {
                emotion_gauge += 1.5f;
            }
            Destroy(other.gameObject); //아이템 파괴
        }
        else if (other.CompareTag("EmotionItem"))
        { // 감정게이지 +
            StartCoroutine(Emotion_Plus());
            int cg;
            if (GameManager.GM.nstate == 2) cg = 11;
            else cg = 13;
            if (emotion_gauge >= 100 - cg)
            {
                emotion_gauge = max_emotion_gauge;
            }
            else
            {
                emotion_gauge += cg;
            }
            Destroy(other.gameObject); //아이템 파괴
        }
        else if (other.CompareTag("ITEMMINUS"))
        { // 성취도 -
            change_value = other.gameObject.GetComponent<ItemSetting>().num * (1.5f - Emotion_gauge_percent / 2);
            if (achieve_gauge + change_value <= 0) achieve_gauge = 0;
            else //감정게이지의 비율따라 추가로 감소하거나 덜 감소함
            {
                achieve_gauge += change_value;
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ITEMPLUS"))
        { // 성취도 +
            StartCoroutine(Acheive_Plus());
            change_value = other.gameObject.GetComponent<ItemSetting>().num * (Emotion_gauge_percent / 2 + 0.5f);
            if (achieve_gauge + change_value >= max_achieve_gauge)
            {
                achieve_gauge = max_achieve_gauge;
            }
            else //감정게이지 비율따라 추가로 증가하거나 덜 증가함
            {
                achieve_gauge += change_value;
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ITEMADULT"))
        {
            StartCoroutine(Acheive_Plus());
            if (achieve_gauge >= 99)
            {
                achieve_gauge = max_achieve_gauge;
            }
            else
            {
                achieve_gauge += 5;
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ENEMYADULT"))
        {
            StartCoroutine("Player_blink");
            if (achieve_gauge <= 1)
            {
                achieve_gauge = 0;
            }
            else
            {
                achieve_gauge -= 5;
            }
        }
        else if (other.tag == "Right_Answer" || other.tag == "Wrong_Answer")
        {

            GameManager.GM.ShowAnswer = true;
            if (other.tag == "Right_Answer")
            {
                GameManager.GM.Correct = true;
            }
        }
        else if (other.CompareTag("Left") || other.CompareTag("Down") || other.CompareTag("Up") || other.CompareTag("Right"))
        {
            if (!GameManager.GM.minigameEnd)
            {
                // 미니게임 화살표 아이템
                int arrow_num = 4;

                switch (other.tag)
                {
                    case "Left":
                        arrow_num = 0;
                        break;
                    case "Down":
                        arrow_num = 1;
                        break;
                    case "Up":
                        arrow_num = 2;
                        break;
                    case "Right":
                        arrow_num = 3;
                        break;
                }
                

                GameManager.GM.minigame_check(arrow_num); // 맞게 먹었는지 체크 
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("MinigameFlag"))
        {
            GameManager.GM.start_minigame();
        }

    }

    IEnumerator Player_blink()
    { // 캐릭터 깜빡임 효과 
        int blinkTime = 0;
        while (blinkTime < 6)
        {
            if (blinkTime % 2 == 0)
            {
                player_sprite.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                player_sprite.color = new Color32(255, 255, 255, 255);
            }
            yield return new WaitForSeconds(0.1f);
            blinkTime++;
        }
        player_sprite.color = new Color32(255, 255, 255, 255);
        yield return null;
    }

    IEnumerator Emotion_Plus(){
        anim.SetTrigger("plusItem");
        emotionUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        emotionUI.SetActive(false);
    }

    IEnumerator Acheive_Plus(){
        anim.SetTrigger("plusItem");
        acheiveUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        acheiveUI.SetActive(false);
    }
}
