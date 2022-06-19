using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Variables

    public GameObject pauseMenu;
    public GameObject camera;

    void Update()
    {
        //Pause menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.active)
            {              
                ResumeGame();
            }
            else
            {               
                PauseGame();               
            }
        }
    }

   
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        camera.GetComponent<MouseLook>().enabled = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        camera.GetComponent<MouseLook>().enabled = true;
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
    }
}
