using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()               //run start game void which should have loading game scene
    {
        Debug.Log("startGame");
        SceneManager.LoadScene("Main");
    }

    public void StartSanbox()               //run start game void which should have loading game scene
    {
        Debug.Log("startSand");
        SceneManager.LoadScene("Test Scene");
    }

    public void SettingsGame()            //run settings void which should have loading settings scene
    {
        Debug.Log("Settings");
        SceneManager.LoadScene("SettingsMenu");
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()               //quit game
    {
        Application.Quit();


        //////////////////////////////////////////////////          <<<---- COMMENT THIS OUT BEFORE BUILDING THE PROJECT!!!!!

        //UnityEditor.EditorApplication.isPlaying = false;          <<<---- COMMENT THIS OUT BEFORE BUILDING THE PROJECT!!!!!

        //////////////////////////////////////////////////          <<<---- COMMENT THIS OUT BEFORE BUILDING THE PROJECT!!!!!
    }
}
