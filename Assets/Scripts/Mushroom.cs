using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Mushroom : MonoBehaviour
{
    public int scoreValue = 1;
    public Button collectMushroomButton;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            collectMushroomButton.gameObject.SetActive(true);
           

        }
    }

    public void MushroomCollected()
    {
        gameObject.SetActive(false);

        Debug.Log("Mushroom collected!");
        SummerGame.Instance.GetPoint(scoreValue);
        collectMushroomButton.gameObject.SetActive(false);

    }
    public void AssignButton(Button button)
    {
        collectMushroomButton = button;
        collectMushroomButton.onClick.AddListener(() => MushroomCollected());

    }

}
