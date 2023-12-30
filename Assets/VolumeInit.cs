using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    public string volumeparam = "Master";
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeparam, 0);
        mixer.SetFloat(volumeparam, volumeValue);
    }

}
