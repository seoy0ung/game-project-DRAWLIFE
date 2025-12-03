using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAppear : MonoBehaviour
{
    private void OnEnable() {
        StartCoroutine(Appear());
    }

    IEnumerator Appear(){
        while(gameObject.GetComponent<Image>().color.a < 1f){
            gameObject.GetComponent<Image>().color += new Color(0, 0, 0, 0.25f);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
