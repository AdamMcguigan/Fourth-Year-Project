using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()               //run start game void which should have loading game scene
    {
        Debug.Log("startGame");
        SceneManager.LoadScene("SampleScene");
    }
    public void SettingsGame()            //run settings void which should have loading settings scene
    {
        Debug.Log("Settings");
        SceneManager.LoadScene("SettingsMenu");
    }
    public void BackGame()                //run back void which should go back to menu
    {
        Debug.Log("back");
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()               //run settings void which should quit game
    {
        Application.Quit();


        //////////////////////////////////////////////////          <<<---- COMMENT THIS OUT BEFORE BUILDING THE PROJECT!!!!!

        //UnityEditor.EditorApplication.isPlaying = false;          <<<---- COMMENT THIS OUT BEFORE BUILDING THE PROJECT!!!!!

        //////////////////////////////////////////////////          <<<---- COMMENT THIS OUT BEFORE BUILDING THE PROJECT!!!!!
    }
}
