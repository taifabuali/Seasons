using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummerGame : MonoBehaviour
{
    public static SummerGame Instance;

    public GameObject mushroomPrefab;
    public Vector3[] mushroomPositions;

    public GameObject hintBoard;
    public Text hintMessage;
    public GameObject hintPanel;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public Text timerText;
    public Text scoreText;

    public float gameDuration = 420f;

    public int mushroomsCollected = 0;
    public float timer;
    private bool hintShown = false;
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
        mushroomsCollected = 0;
        gameActive = true;

        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";

        
        StartCoroutine(UpdateTimer());

    }
    


    public void ResetGame()
    {
        timer = gameDuration;
        mushroomsCollected = 0;
        gameActive = true;
        hintPanel.gameObject.SetActive(false);
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
            timer -= Time.deltaTime ;
            timerText.text = "Time Left:" + Mathf.Floor(timer/60).ToString()+ " Minutes";
            if (timer <= 0)
            {
                EndGame(false);
            }

            yield return null;

        }
    }
    public void ShowHint(string hint)
    {
        hintMessage.text = hint;
        StartCoroutine(HintEnable());
        hintShown = true;
        //CreateMushroom(hintShown);

    }

    public void CollectMushroom(GameObject mushroom)
    {
        Destroy(mushroom);

        mushroomsCollected++;
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
        StartCoroutine(endEnable());

        Manager.Instance.OnSummerGameEnded(success);

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
    //public void CreateMushroom(bool hint)
    // { 
    //if(hint)
    //     foreach (Vector3 position in mushroomPositions)
    //{
    //   if (hintShown)
    //    Instantiate(mushroomPrefab, position, Quaternion.identity);
    // }
    //  }
}