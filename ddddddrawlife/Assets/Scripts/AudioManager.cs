using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float effectVol=1f;
    public float BGMVol=0.2f;
    public Slider BGMSlider;
    public Slider effectSlider;
    public AudioSource BGMSource;
    public AudioSource effectSource;
    public float BGMSave;
    public float effectSave;
    
    // Start is called before the first frame update
    void Start()
    {
        BGMVol = PlayerPrefs.GetFloat("BGM", 0.2f);
        BGMSlider.value = BGMVol;
        BGMSource.volume = BGMVol;

        effectVol = PlayerPrefs.GetFloat("effect", 1f);
        effectSlider.value = effectVol;
        effectSource.volume = effectVol;
    }

    public void SoundSlider()
    {
        BGMSource.volume = BGMSlider.value;
        BGMVol = BGMSource.volume;
        PlayerPrefs.SetFloat("BGM", BGMVol);

        effectSource.volume = effectSlider.value;
        effectVol = effectSource.volume;
        PlayerPrefs.SetFloat("effect", effectVol);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
