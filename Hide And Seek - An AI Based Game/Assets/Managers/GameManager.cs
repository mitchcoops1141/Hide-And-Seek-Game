using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);


        for (int i = 0; i < totalCharacters; i++)
        {
            charUnlocks.Add(false);
        }

        charUnlocks[0] = true;

    }

    public int seekerLevelIndex = 1;
    public int hiderLevelIndex = 1;

    public int totalCharacters = 14;
    public List<bool> charUnlocks = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
