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
        //Cursor.visible = enable;

        //Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);


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
}
