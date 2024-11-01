using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AutumnGame : MonoBehaviour,IGameCycle
{
    public static AutumnGame Instance;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public Text timerText;
    public Text scoreText;
    int score = 0;  

    public float gameDuration = 420f;

    public float timer;
    private bool gameActive = true;

    public GameObject GuidePanel;
    public Text guideText;
    float time = 5f;
    public GameObject target;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        timer = gameDuration;
        gameActive = true;
        target.SetActive(true);
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";
       
        StartCoroutine(GuideEnable(time));

        StartCoroutine(UpdateTimer());

    }
   
    public void GetPoint()
    {

        score += 50;
        scoreText.text = "Score: " + score;

        if (score >= 300)
        {
            EndGame(true);
        }
    }

    public void ResetGame()
    {
        timer = gameDuration;
        gameActive = true;
        target.SetActive(true);    
        scoreText.text = "Score: 0";
        gameOverPanel.SetActive(false);
        StartCoroutine(GuideEnable(time));

        StartCoroutine(UpdateTimer());
    }
    IEnumerator UpdateTimer()
    {
        while (gameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time Left:" + Mathf.Floor(timer / 60).ToString() + " Minutes";
            if (timer <= 0)
            {
                EndGame(false);
            }

            yield return null;

        }
    }
   
    public void EndGame(bool success)
    {
        gameActive = false;
        gameOverText.text = success ? "Great you got the target points!" : "Time's up! You lose.";
        StartCoroutine(endEnable());
        target.SetActive(false);
        Manager.Instance.OnGameEnded(success,Manager.Season.Autumn, Manager.Season.Winter,Manager.Instance.winterGameObject);

    }


    IEnumerator endEnable()
    {
        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(5);
        gameOverPanel.SetActive(false);

    }
   public IEnumerator GuideEnable(float time)
    {
        guideText.text = "Now Roben Hood show me your talent in archery...";
        yield return new WaitForSeconds(time - 2);
        
        GuidePanel.SetActive(true);
        yield return new WaitForSeconds(time - 4);

        guideText.text = "Make 300 points to win this game so the winter come...";
        yield return new WaitForSeconds(time - 2);
        guideText.text = "Good Luck..!";
        yield return new WaitForSeconds(time);

        GuidePanel.SetActive(false);

    }
   
}
