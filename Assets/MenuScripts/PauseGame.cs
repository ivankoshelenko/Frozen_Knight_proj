using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused { get; private set; }
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] public Player character;
    public static PlayerInput pauseInput;
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject guideFirstButton;
    [SerializeField] private GameObject settingsFirstButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private GameObject mainPanel;
    public enum Panel { Main, Guide, Settings }
    public Panel currentPanel;

    private void Awake()
    {
        pauseInput = new PlayerInput();
    }

    private void Start()
    {
        _pauseMenuUI = transform.Find("PauseMenu").gameObject;
        //firstButton = transform.Find("ResumeButton").gameObject;
        pauseInput.Player.Pause.performed += OnPauseKeyPressed;
        pauseInput.Enable();
        Debug.Log(pauseInput);
    }


    private void OnPauseKeyPressed(InputAction.CallbackContext obj) => SwitchPauseMenu();

    private void SwitchPauseMenu()
    {
        Debug.Log("switch");
        if (GameIsPaused)
            Resume();
        else
            Pause();
    }

    private void Pause()
    {
        OpenPauseMenu(true);
    }

    public void Resume()
    {
        OpenPauseMenu(false);
    }

    public void Quit()
    {
        const int mainMenuIndex = 0;
        OpenPauseMenu(false);
        SceneManager.LoadScene(mainMenuIndex);
    }

    private void OpenPauseMenu(bool enable)
    {
        _pauseMenuUI.SetActive(enable);
        //Cursor.visible = false;

        //Cursor.lockState = CursorLockMode.Locked;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);

        if (currentPanel == Panel.Guide)
            guidePanel.gameObject.SetActive(false);
        if (currentPanel == Panel.Settings)
            settingsPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);

        Time.timeScale = enable ? 0f : 1f;
        GameIsPaused = enable;
        if (enable)
            character.input.Disable();
        else character.input.Enable();
    }

    private void OnEnable() => pauseInput.Enable();

    private void OnDisable() => pauseInput.Disable();
    public static void Reset()
    {

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

        EventSystem.current.SetSelectedGameObject(firstButton);
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

        EventSystem.current.SetSelectedGameObject(firstButton);
        currentPanel = Panel.Main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (currentPanel == Panel.Main)
                EventSystem.current.SetSelectedGameObject(firstButton);
            if (currentPanel == Panel.Guide)
                EventSystem.current.SetSelectedGameObject(guideFirstButton);
            if (currentPanel == Panel.Settings)
                EventSystem.current.SetSelectedGameObject(settingsFirstButton);
        }
    }
}
