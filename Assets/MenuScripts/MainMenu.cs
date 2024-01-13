using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainFirstButton,guideFirstButton, settingsFirstButton;
    public GameObject settingsPanel, guidePanel;
    public enum Panel { Main, Guide, Settings }
    public Panel currentPanel;
    public void Start()
    {
        //FindObjectOfType<AudioManager>().Play("MainTheme");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentPanel = Panel.Main;
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
        currentPanel = Panel.Settings;
    }

    public void CloseSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(mainFirstButton);
        currentPanel = Panel.Main;
    }
    public void OpenGuide()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(guideFirstButton);
        currentPanel = Panel.Guide;
    }

    public void CloseGuide()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(mainFirstButton);
        currentPanel = Panel.Main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if(currentPanel == Panel.Main)
                EventSystem.current.SetSelectedGameObject(mainFirstButton);
            if(currentPanel == Panel.Guide)
                EventSystem.current.SetSelectedGameObject(guideFirstButton);
            if (currentPanel == Panel.Settings)
                EventSystem.current.SetSelectedGameObject(settingsFirstButton);
        }
    }
}
