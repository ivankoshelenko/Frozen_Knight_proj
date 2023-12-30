using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    // Start is called before the first frame update
    public string volumeParameter = "Master";
    public AudioMixer mixer;
    public Slider slider;
    private float volume;
    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
        slider.value = 1f;
        Debug.Log(PlayerPrefs.GetFloat(volumeParameter, volume));
    }
    public void HandleSliderValueChanged(float value)
    {
        volume = Mathf.Log10(value) * 20f;
        mixer.SetFloat(volumeParameter, volume);
    }
    void Start()
    {
        volume = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) * 20f);
        slider.value = Mathf.Pow(10f, volume / 20f);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, volume) ;
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat(volumeParameter));
    }

}
