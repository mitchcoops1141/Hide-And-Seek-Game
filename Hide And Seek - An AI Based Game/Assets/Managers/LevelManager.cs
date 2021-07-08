using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [Header("Player Conection")]
    public Player_Cs player;

    [Header("Seeker Conenctions")]
    public Seeker[] seekers;

    [Header("Round Duration Timer")]
    public float roundTimer;
    [SerializeField] private TextMeshProUGUI roundTimerText;

    [Header("Round Starting Timer")]
    [SerializeField] private TextMeshProUGUI startTimerText;
    private int startTimer = 3;

    [Header("Type of Level (S or H)")]
    public bool isSeeker;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        //start the start round timer countdown funcction
        StartCoroutine("StartTimer");
    }

    void BeginGame()
    {
        //allow seekers to function
        foreach (Seeker seeker in seekers)
        {
            seeker.BeginSeeking();
        }

        //allow player to function
        player.canFunction = true;

        //start the round timer countdown function
        StartCoroutine("RoundTimer");
    }

    void EndGame()
    {
        //stop seekers from functioning
        foreach (Seeker seeker in seekers)
        {
            seeker.StopSeeking();
        }

        //stop player from functioning
        player.canFunction = false;
    }

    IEnumerator Win()
    {
        startTimerText.text = "YOU WIN!";

        if (isSeeker)
            GameManager.instance.seekerLevelIndex++;
        else
            GameManager.instance.hiderLevelIndex++;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MenuScene");
    }

    public void Lose()
    {
        StartCoroutine("LoseUI");
    }

    IEnumerator LoseUI()
    {
        EndGame();
        startTimerText.text = "FINISH";

        yield return new WaitForSeconds(1f);

        

        startTimerText.text = "YOU LOST";

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator StartTimer()
    {
        //hide the round timer
        roundTimerText.text = "";

        //while the timer is not at 0 -> set the text to the timer value, wait 1 second, and reduce the timer value
        while(startTimer > 0)
        {
            startTimerText.text = startTimer.ToString();

            yield return new WaitForSeconds(1f);

            startTimer--;
        }

        //start the game
        startTimerText.text = "GO!";
        BeginGame();

        //wait 1 second
        yield return new WaitForSeconds(1f);

        //hide the text
        startTimerText.text = "";
    }

    IEnumerator RoundTimer()
    {
        //while the timer is not at 0 -> set the text to the timer value, wait 1 second, and reduce the timer value
        while (roundTimer > 0)
        {
            roundTimerText.text = roundTimer.ToString();

            yield return new WaitForSeconds(1f);

            roundTimer--;
        }

        //end the game
        roundTimerText.text = "0";
        startTimerText.text = "FINISH";
        EndGame();
        yield return new WaitForSeconds(1f);
        StartCoroutine("Win");
    }
}
