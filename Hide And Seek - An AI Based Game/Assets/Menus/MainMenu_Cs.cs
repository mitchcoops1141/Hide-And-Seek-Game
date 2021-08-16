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
    public GameObject characterSelectScreen;
    public GameObject background;

    [Header("Character Select")]
    public GameObject character;

    public Transform leftPos;
    public Transform rightPos;
    public Transform middlePos;

    public Button leftBtn;
    public Button rightBtn;

    public Text characterText;

    public MeshSaver meshSaver;

    int characterIndex = 0;
    GameObject characterToInstantiate;
    float speed = 2.5f;
    bool shouldDance = true;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (leftPos.childCount > 0)
        {
            Transform t = leftPos.GetChild(0);

            if (t.localPosition != Vector3.zero)
            {
                t.localPosition = Vector3.MoveTowards(t.localPosition, Vector3.zero, speed * Time.deltaTime);
                var targetRotation = Quaternion.LookRotation(new Vector3(90, 0, 0) - leftPos.GetChild(0).position);
                leftPos.GetChild(0).rotation = Quaternion.Slerp(leftPos.GetChild(0).rotation, targetRotation, 8 * Time.deltaTime);
            }
            else
            {
                leftBtn.enabled = true;
                rightBtn.enabled = true;
                Destroy(t.gameObject);
            }
        }

        if (middlePos.childCount > 0)
        {
            Transform t = middlePos.GetChild(0);

            if (t.localPosition != Vector3.zero)
            {
                t.localPosition = Vector3.MoveTowards(t.localPosition, Vector3.zero, speed * Time.deltaTime);
            }
            else
            {
                var targetRotation = Quaternion.LookRotation(new Vector3(0, 0, 0) - middlePos.GetChild(0).position);
                middlePos.GetChild(0).rotation = Quaternion.Slerp(middlePos.GetChild(0).rotation, targetRotation, 8 * Time.deltaTime);
                middlePos.GetChild(0).GetComponentInChildren<Animator>().SetBool("ShouldWalk", false);
                if (shouldDance)
                    StartCoroutine("Dance", middlePos.GetChild(0).GetComponentInChildren<Animator>());
            }
        }

        if (rightPos.childCount > 0)
        {
            Transform t = rightPos.GetChild(0);

            if (t.localPosition != Vector3.zero)
            {
                t.localPosition = Vector3.MoveTowards(t.localPosition, Vector3.zero, speed * Time.deltaTime);
                var targetRotation = Quaternion.LookRotation(new Vector3(-90, 0, 0) - rightPos.GetChild(0).position);
                rightPos.GetChild(0).rotation = Quaternion.Slerp(rightPos.GetChild(0).rotation, targetRotation, 8 * Time.deltaTime);
            }
            else
            {
                leftBtn.enabled = true;
                rightBtn.enabled = true;
                Destroy(t.gameObject);
            }
        }
    }

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

    #region Showing and Hiding Screens
    public void MainMenuScene()
    {
        Textures.SetActive(true);
        mainMenuScreen.SetActive(true);
        creditsScreen.SetActive(false);
        gameModeScreen.SetActive(false);
        characterSelectScreen.SetActive(false);
        background.SetActive(true);
    }

    public void CreditsScreen()
    {
        Textures.SetActive(false);
        mainMenuScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void CharacterSelectScreen()
    {
        PlaySelectSound();
        Textures.SetActive(false);
        mainMenuScreen.SetActive(false);
        seekerLevelSelectScreen.SetActive(false);
        hiderLevelSelectScreen.SetActive(false);
        gameModeScreen.SetActive(false);
        characterSelectScreen.SetActive(true);
        background.SetActive(false);

        if (characterToInstantiate)
            characterToInstantiate.GetComponent<Character>().UpdateSkin(characterIndex);
        else
        {
            characterToInstantiate = Instantiate(character.gameObject, middlePos);
            characterToInstantiate.GetComponent<Character>().UpdateSkin(characterIndex);
        }
    }

    public void GameModeScreen()
    {
        PlaySelectSound();
        Textures.SetActive(false);
        mainMenuScreen.SetActive(false);
        seekerLevelSelectScreen.SetActive(false);
        hiderLevelSelectScreen.SetActive(false);
        characterSelectScreen.SetActive(false);
        gameModeScreen.SetActive(true);
        background.SetActive(true);
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
        meshSaver.SetMesh(character.GetComponent<Character>().skins[characterIndex]);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            LoadingBar.value = progress;

            yield return null;
        }
    }

    #endregion

    public void NextCharacterRight()
    {
        leftBtn.enabled = false;
        rightBtn.enabled = false;

        if (characterIndex == character.GetComponent<Character>().skins.Length - 1)
            characterIndex = 0;
        else
            characterIndex++;

        characterText.text = character.GetComponent<Character>().skinNames[characterIndex];

        GameObject obj = Instantiate(character, leftPos);
        obj.GetComponent<Character>().UpdateSkin(characterIndex);
        
        if (middlePos.childCount > 0)
        {
            middlePos.GetChild(0).GetComponentInChildren<Animator>().SetBool("ShouldWalk", true);
            middlePos.GetChild(0).parent = rightPos;
        }

        if (leftPos.childCount > 0)
        {
            leftPos.GetChild(0).GetComponentInChildren<Animator>().SetBool("ShouldWalk", true);
            leftPos.GetChild(0).rotation = Quaternion.Euler(0, -90, 0);
            leftPos.GetChild(0).parent = middlePos;
        }
    }

    public void NextCharacterLeft()
    {
        leftBtn.enabled = false;
        rightBtn.enabled = false;

        if (characterIndex == 0)
            characterIndex = character.GetComponent<Character>().skins.Length - 1;
        else
            characterIndex--;

        characterText.text = character.GetComponent<Character>().skinNames[characterIndex];

        GameObject obj = Instantiate(character, rightPos);
        obj.GetComponent<Character>().UpdateSkin(characterIndex);

        if (middlePos.childCount > 0)
        {
            middlePos.GetChild(0).GetComponentInChildren<Animator>().SetBool("ShouldWalk", true);
            middlePos.GetChild(0).parent = leftPos;
        }

        if (rightPos.childCount > 0)
        {
            rightPos.GetChild(0).GetComponentInChildren<Animator>().SetBool("ShouldWalk", true);
            rightPos.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
            rightPos.GetChild(0).parent = middlePos;
        }
    }

    IEnumerator Dance(Animator anim)
    {
        shouldDance = false;

        yield return new WaitForSeconds(Random.Range(5, 25));

        //dance
        if (anim)
        {
            anim.SetTrigger("ShouldDance");

            //wait animation time
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length + 5);
        }
            

        shouldDance = true;
    }
}
