using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AutumnGame : MonoBehaviour,IGameCycle
{
    public static AutumnGame Instance;
    public bool IsActive { get; private set; }
    public GameObject player;
    int score = 0;
    public float gameDuration = 420f;
    private bool gameActive = true;
    public float timer;

    [Header("UI")]
    public GameObject gameOverPanel;
    public Text gameOverText;

    public Text timerText;
    public Text scoreText;
 

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

    }
    void Start()
    {
        
        timer = gameDuration;
        gameActive = true;
        target.SetActive(true);
        bow.SetActive(true);    
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";

        PositionPlayerInFrontOfTarget();
        StartCoroutine(UpdateTimer());
        IsActive = true; // Mark this game as active
        GuideManager.Instance.guideButton.gameObject.SetActive(true); // Show guide button
    }
    void PositionPlayerInFrontOfTarget()
    {
        Vector3 targetPosition = target.transform.position;
        //targetPosition + target.transform.forward * 5; 
        player.transform.position = new Vector3(276f, 0f, 179.1f);
        player.transform.rotation = Quaternion.Euler(0f, 92.5f, 0f);
        player.transform.LookAt(targetPosition);
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
        IsActive = false; // Mark this game as inactive
        GuideManager.Instance.guideButton.gameObject.SetActive(false);
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
   
   
   
}
