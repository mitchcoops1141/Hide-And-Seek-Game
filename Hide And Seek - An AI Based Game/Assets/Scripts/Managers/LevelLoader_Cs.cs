using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader_Cs : MonoBehaviour
{
    public Animator Transition;
    public float TransitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        Transition.SetTrigger("Start");
        
        //Wait
        yield return new WaitForSeconds(TransitionTime);
        
        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
}
