using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    PlayerControls controls;
    private bool isPaused = false;
    public GameObject PauseMenuUI;
    // Start is called before the first frame update
    private void Awake()
    {
        controls = new PlayerControls();
    }
    // Update is called once per frame
    void Update()
    {
        controls.UI.Pause.performed += ctx => Pause();
    }

private void Pause()
    {
        Debug.Log("Pause");
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            PauseMenuUI.SetActive(true);
        } else
        {
            Time.timeScale = 1;
            isPaused = false;
            PauseMenuUI.SetActive(false);
        }
    }

    //Pause Menu Buttons
    public void LoadScene(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
