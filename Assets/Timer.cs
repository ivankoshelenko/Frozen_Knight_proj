using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeOfDay;
    public int Day = 1;
    public float dayDuration = 120f;
    [SerializeField] private TextMeshProUGUI _timeDisplay;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeDisplay.text = ((int)(dayDuration - Time.timeSinceLevelLoad)).ToString();
        if (dayDuration - Time.timeSinceLevelLoad <= 0)
            SceneManager.LoadScene(0);
    }
}
