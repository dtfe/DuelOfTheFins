using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    PlayerControls controls;
    private bool isPaused = false;

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
        } else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
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
