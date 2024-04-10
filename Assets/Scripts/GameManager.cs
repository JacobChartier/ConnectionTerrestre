using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        InputManager.controls.Player.PauseMenu.canceled += PauseMenu_performed;

        InputManager.controls.Menus.Close.performed += Close_performed;
    }

    private void Close_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => MenuHandler.Instance.GetMenu<PauseMenuUI>().Hide();

    private void PauseMenu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => MenuHandler.Instance.GetMenu<PauseMenuUI>().Show();

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
