using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_Cs : MonoBehaviour
{
    public GameObject Textures;
    public GameObject mainMenuScreen;
    public GameObject creditsScreen;
    public GameObject gameModeScreen;
    public GameObject seekerLevelSelectScreen;
    public GameObject hiderLevelSelectScreen;
    public GameObject LoadingScreen;
    public Slider LoadingBar;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlaySelectSound()
    {
        FindObjectOfType<AudioManager_Cs>().Play("Select");
    }

    public void GobackSound()
    {
        FindObjectOfType<AudioManager_Cs>().Play("GoBack");
    }

    public void GoMehSound()
    {
        FindObjectOfType<AudioManager_Cs>().Play("Meh");
    }

    public void MainMenuScene()
    {
        Textures.SetActive(true);
        mainMenuScreen.SetActive(true);
        creditsScreen.SetActive(false);
        gameModeScreen.SetActive(false);
    }

    public void CreditsScreen()
    {
        Textures.SetActive(false);
        mainMenuScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void GameModeScreen()
    {
        PlaySelectSound();
        Textures.SetActive(false);
        mainMenuScreen.SetActive(false);
        seekerLevelSelectScreen.SetActive(false);
        hiderLevelSelectScreen.SetActive(false);
        gameModeScreen.SetActive(true);
    }

    public void SeekerGameMode()
    {
        PlaySelectSound();
        seekerLevelSelectScreen.SetActive(true);
        gameModeScreen.SetActive(false);
    }

    public void HiderGameMode()
    {
        PlaySelectSound();
        hiderLevelSelectScreen.SetActive(true);
        gameModeScreen.SetActive(false);
    }

    public void PlayLevelSeeker(int levelNumber)
    {
        PlaySelectSound();
        StartCoroutine(LoadAsynchronously("SeekerLevel" + levelNumber.ToString()));
    }

    public void PlayLevelHider(int levelNumber)
    {
        PlaySelectSound();
        StartCoroutine(LoadAsynchronously("HiderLevel" + levelNumber.ToString()));
    }

    IEnumerator LoadAsynchronously (string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        LoadingScreen.SetActive(true);
        hiderLevelSelectScreen.SetActive(false);
        seekerLevelSelectScreen.SetActive(false);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            LoadingBar.value = progress;

            yield return null;
        }
    }
}
