using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    public static bool skip_tutorial = false;

    public void tutorial_toggle(bool input){
        skip_tutorial = input;
        Debug.Log(skip_tutorial);
    }
}
