using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager2 : MonoBehaviour
{
    public float effectVol = 1f;
    public float BGMVol = 0.2f;
    public AudioSource BGMSource;
    public AudioSource[] effectSource;

    // Start is called before the first frame update
    void Start()
    {
        BGMSource.volume = PlayerPrefs.GetFloat("BGM", 0.2f);
        effectSource[0].volume = PlayerPrefs.GetFloat("effect", 1f);
        effectSource[1].volume = PlayerPrefs.GetFloat("effect", 1f);

    }

}
