using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Cs : MonoBehaviour
{
    public GameObject Textures;
    public GameObject Screen1;
    public GameObject Screen2;
    public void StartGame()
    {
        FindObjectOfType<AudioManager_Cs>().Play("Select");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnButtonEnter()
    {
        FindObjectOfType<AudioManager_Cs>().Play("Select");
    }

    public void ReturnScreen1()
    {
        Textures.SetActive(true);
        Screen1.SetActive(true);
        Screen2.SetActive(false);
    }

    public void CreditsGame()
    {
        Textures.SetActive(false);
        Screen1.SetActive(false);
        Screen2.SetActive(true);
    }
}
