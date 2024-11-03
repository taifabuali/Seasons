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

    public GameObject GuidePanel;
    public Text guideText;

    public Button guideButton;

    public float gameDuration = 420f;

    public float timer;
    private bool gameActive = true;
    float time = 10f;

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
        mushroomsCollected = 0;
        gameActive = true;

        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";



        if (guideButton != null)
        {
         guideButton.onClick.AddListener(() =>   StartCoroutine(GuideEnable(time)));
        }

    }



    public void ResetGame()
    {
        timer = gameDuration;
        mushroomsCollected = 0;
        gameActive = true;
        StartCoroutine(GuideEnable(time));
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
        mushroomsCollected+= points;
        scoreText.text = "Score: " + mushroomsCollected;

        if (mushroomsCollected >= mushroomPositions.Length)
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
        StartCoroutine(endEnable());
        Manager.Instance.OnGameEnded(success,Manager.Season.Summer,Manager.Season.Autumn,Manager.Instance.autumnGameObject);
        Manager.Instance._player.transform.position = Manager.Instance.autumnGameObject.transform.position;

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
           "Go tothe blue hint board to get hint about mushrrom places...","You must find them all before the time up...", "Good Luck..!"
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