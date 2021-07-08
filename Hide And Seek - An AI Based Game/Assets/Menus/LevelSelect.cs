using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject[] levels;
    public bool isSeeker;

    // Start is called before the first frame update
    void Start()
    {
        if (isSeeker)
        {
            //set the colors of all the levels depending on the seeker level index
            //set the interactability of each level based on the seeker level index
            for (int i = 0; i < levels.Length; i++)
            {
                if (i == GameManager.instance.seekerLevelIndex - 1)
                {
                    levels[i].GetComponent<Image>().color = Color.white;
                    levels[i].GetComponent<Button>().interactable = true;
                }
                else if (i < GameManager.instance.seekerLevelIndex - 1)
                {
                    levels[i].GetComponent<Image>().color = Color.green;
                    levels[i].GetComponent<Button>().interactable = true;
                }
                else if (i > GameManager.instance.seekerLevelIndex - 1)
                {
                    levels[i].GetComponent<Image>().color = Color.red;
                    levels[i].GetComponent<Button>().interactable = false;
                }
            }
        }
        else
        {
            //set the colors of all the levels depending on the seeker level index
            //set the interactability of each level based on the seeker level index
            for (int i = 0; i < levels.Length; i++)
            {
                if (i == GameManager.instance.hiderLevelIndex - 1)
                {
                    levels[i].GetComponent<Image>().color = Color.white;
                    levels[i].GetComponent<Button>().interactable = true;
                }
                else if (i < GameManager.instance.hiderLevelIndex - 1)
                {
                    levels[i].GetComponent<Image>().color = Color.green;
                    levels[i].GetComponent<Button>().interactable = true;
                }
                else if (i > GameManager.instance.hiderLevelIndex - 1)
                {
                    levels[i].GetComponent<Image>().color = Color.red;
                    levels[i].GetComponent<Button>().interactable = false;
                }
            }
        }
        
    }
}
