using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

    }

    private void Start()
    {
        MenuHandler.GetMenu<PauseMenuUI>().Hide();
    }

    private void Close_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => MenuHandler.GetMenu<PauseMenuUI>().Hide();

    private void PauseMenu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => MenuHandler.GetMenu<PauseMenuUI>().Show();

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GameObject.Find("MainMenu").GetComponent<Button>().onClick.AddListener(() => Instance.SwitchToMainMenu());
        InputManager.controls.Player.PauseMenu.performed += PauseMenu_performed;
        InputManager.controls.Menus.Close.performed += Close_performed;

        MenuHandler.GetMenu<PauseMenuUI>().Hide();

        if (arg0 == SceneManager.GetSceneByName("World"))
            Player.Instance.LoadInventory(true);
        else 
            Player.Instance.LoadInventory();
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

public enum Scenes
{
    MAIN_MENU,
    WORLD,
    COMBAT,
    END,

    INVALID = -1,
    DEBUG = -2
}