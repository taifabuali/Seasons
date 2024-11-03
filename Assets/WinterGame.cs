using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinterGame : MonoBehaviour,IGameCycle
{
  public static WinterGame Instance;

    int score = 0;
    public float gameDuration = 420f;
    public float timer;
    private bool gameActive = true;
    float time = 5f;

    [Header("UI")]
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text timerText;
    public Text scoreText;
    public GameObject GuidePanel;
    public Text guideText;

   

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
    // Start is called before the first frame update
    void Start()
    {
        timer = gameDuration;
        gameActive = true;
       
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";

        StartCoroutine(GuideEnable(time));

        StartCoroutine(UpdateTimer());

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
        Manager.Instance.OnGameEnded(success, Manager.Season.Winter, Manager.Season.Spring, Manager.Instance.springGameObject);

    }


    IEnumerator endEnable()
    {
        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(5);
        gameOverPanel.SetActive(false);

    }
    public IEnumerator GuideEnable(float time)
    {
        guideText.text = "Winter is Arraived..";
        yield return new WaitForSeconds(time - 2);

        GuidePanel.SetActive(true);
        yield return new WaitForSeconds(time - 4);

        guideText.text = "Try to get to the finsh line first..";
        yield return new WaitForSeconds(time - 2);
        guideText.text = "Good Luck..!";
        yield return new WaitForSeconds(time);

        GuidePanel.SetActive(false);

    }

}
