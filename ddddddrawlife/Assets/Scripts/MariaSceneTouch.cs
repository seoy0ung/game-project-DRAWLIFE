using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaSceneTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() // 화면 터치하면 다음 대사
    {
        CutsceneManager.CM.next_text();
    }
}
