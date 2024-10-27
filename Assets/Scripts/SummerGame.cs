using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject gameOverPanel;

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
        hintMessage.gameObject.SetActive(false);
        mushroomsCollected = 0;
        gameActive = true;

        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";

        foreach (Vector3 position in mushroomPositions)
        {
            Instantiate(mushroomPrefab, position, Quaternion.identity);
        }
        StartCoroutine(UpdateTimer());
    }
    

    public void ResetGame()
    {
        timer = gameDuration;
        mushroomsCollected = 0;
        gameActive = false;
        hintMessage.gameObject.SetActive(false);
        scoreText.text = "Score: 0";
        gameOverPanel.SetActive(false);
        GameObject[] mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");
        foreach (GameObject mushroom in mushrooms)
        {
            Destroy(mushroom);

        }


    }
    IEnumerator UpdateTimer()
    {
        while (gameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time Left: " + Mathf.Floor(timer).ToString() + "seconds";
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
        hintMessage.gameObject.SetActive(true);
        hintShown = true;
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

    void EndGame(bool success)
    {
        gameActive = false;
        gameOverPanel.SetActive(true);
        hintMessage.text = success ? "You collected all mushrooms!" : "Time's up! You lost.";
       
    }

}