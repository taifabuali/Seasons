using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AutumnGame : MonoBehaviour,IGameCycle
{
    public static AutumnGame Instance;

    float time = 10f;
    int score = 0;
    public float gameDuration = 420f;
    private bool gameActive = true;
    public float timer;

    [Header("UI")]
    public GameObject gameOverPanel;
    public Text gameOverText;

    public Text timerText;
    public Text scoreText;
 
    public GameObject GuidePanel;
    public Text guideText;
    public Button guideButton;

    public GameObject target;
    public GameObject bow;
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
        guideButton.gameObject.SetActive(true);

    }
    void Start()
    {
        
        timer = gameDuration;
        gameActive = true;
        target.SetActive(true);
        bow.SetActive(true);    
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";
       

        StartCoroutine(UpdateTimer());

        if (guideButton != null)
        {
            guideButton.onClick.AddListener(() => StartCoroutine(GuideEnable(0)));
        }


    }

    public void GetPoint(int points)
    {

        score += points;
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
        bow.SetActive(false);
        Manager.Instance.OnGameEnded(success,Manager.Season.Autumn, Manager.Season.Winter,Manager.Instance.winterGameObject);

    }


    IEnumerator endEnable()
    {
        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(5);
        gameOverPanel.SetActive(false);

    }
    public void GetGuideByButoon()
    {
      
            StartCoroutine(GuideEnable(0));
        
    }
    IEnumerator GuideEnable(float initialtime)
    {

        GuidePanel.SetActive(true);
        yield return new WaitForSeconds(initialtime);

        string[] message =
        {
          "Now Roben Hood show me your talent in archery...","Make 300 points to win this game so the winter come...", "Good Luck..!"
        };
        float[] messageDuration = { 3, 4, 2 };
        for (int i = 0; i < message.Length; i++)
        {
            guideText.text = message[i];
            yield return new WaitForSeconds(messageDuration[i]);
        }
        GuidePanel.SetActive(false);
    }
   
   
}
