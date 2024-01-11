using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainFirstButton,guideFirstButton, settingsFirstButton;
    public GameObject settingsPanel, guidePanel;
    public void Start()
    {
        //FindObjectOfType<AudioManager>().Play("MainTheme");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
    }

    public void CloseSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }
    public void OpenGuide()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(guideFirstButton);
    }

    public void CloseGuide()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }
}
