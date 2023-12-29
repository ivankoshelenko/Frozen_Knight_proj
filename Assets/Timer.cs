using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeOfDay;
    public static int Day = 1;
    public float dayDuration = 120f;
    [SerializeField] private TextMeshProUGUI _timeDisplay;
    [SerializeField] private SafeZone castle;
    [SerializeField] private TextMeshProUGUI _dayDisplay;
    [SerializeField] private CanvasGroup muffling;

    void Start()
    {
        muffling.alpha = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        _timeDisplay.text = ((int)(dayDuration - Time.timeSinceLevelLoad)).ToString();

        if (dayDuration - Time.timeSinceLevelLoad <= 20 && !castle.isSecure)
        {
            muffling.alpha += 0.04f * Time.deltaTime;
        }
        if (castle.isSecure)
        {
            if (muffling.alpha >= 0.29f)
            muffling.alpha -= 0.04f * Time.deltaTime;
        }

        if (dayDuration - Time.timeSinceLevelLoad <= 0 && castle.isSecure)
        {
            Day += 1;
            _dayDisplay.text = Day.ToString();
            SceneManager.LoadScene(1);
        }
    }
}
