using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.Windows;

public class SummerGame : MonoBehaviour, IGameCycle
{
    public static SummerGame Instance;
    public bool IsActive { get; private set; }
    [Header("Mushroom")]
    public GameObject mushroomPrefab;
    public Vector3[] mushroomPositions;
    public int mushroomsCollected = 0;


    [Header("UI")]
    public GameObject hintBoard;
    public Text hintMessage;
    public GameObject hintPanel;

    public GameObject gameOverPanel;
    public Text gameOverText;
    
    public Text timerText;
    public Text scoreText;

    public float gameDuration = 420f;
    public float timer;
    private bool gameActive = true;

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
        //hintPanel.gameObject.SetActive(false);
        gameActive = true;

        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";

        IsActive = true; // Mark this game as active
        GuideManager.Instance.guideButton.gameObject.SetActive(true); // Show guide button
        StartCoroutine(UpdateTimer());
    }


    public void ResetGame()
    {
        timer = gameDuration;
        mushroomsCollected = 0;
        gameActive = true;
        scoreText.text = "Score: 0";
        gameOverPanel.SetActive(false);
        GameObject[] mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");
        foreach (GameObject mushroom in mushrooms)
        {
            Destroy(mushroom);

        }

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
   


    public void GetPoint(int points)
    {
        mushroomsCollected += points;
        scoreText.text = "Score: " + mushroomsCollected;
        Debug.Log(scoreText.text);
        if (mushroomsCollected >= 3)
        {
            EndGame(true);

        }
    }

    public void EndGame(bool success)
    {
        gameActive = false;
        gameOverText.text = success ? "You collected all mushrooms!" : "Time's up! You lose.";
        mushroomsCollected = 0;
        timer = 0;
        scoreText.text = "Score: 0 ";
        IsActive = false; // Mark this game as inactive
        GuideManager.Instance.guideButton.gameObject.SetActive(false);
        StartCoroutine(endEnable());
        Manager.Instance.OnGameEnded(success,Manager.Season.Summer,Manager.Season.Autumn,Manager.Instance.autumnGameObject);

    }

    IEnumerator HintEnable()
    {
        hintPanel.SetActive(true);

        yield return new WaitForSeconds(5);
        hintPanel.SetActive(false);

    }
    IEnumerator endEnable()
    {
        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(5);
        gameOverPanel.SetActive(false);

    }
    
}