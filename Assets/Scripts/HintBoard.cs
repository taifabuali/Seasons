using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintBoard : MonoBehaviour
{
    public string[] hintMessage = new string[] {
        "There are a triplet of mountains; look near the biggest one under an apple tree...",
        "Near the crossroads under a tree you can find one...",
        "Find a small valley full of red poppies..."
    };

    [Header("UI")]
    public Text hintMessagetext;
    public GameObject hintPanel;
    public Button hintButton;
    public Button mushroomButton;

    public HashSet<int> hintSet = new HashSet<int>();

    [Header("Mushroom")]
    public GameObject mushroomPrefab;
    public Vector3[] mushroomTransform;

    private void Start()
    {
        hintPanel.SetActive(false);
        hintButton.onClick.AddListener(OnHintButtonClicked);
        hintButton.gameObject.SetActive(false); 
    }

    public void ShowHint(string hint, int index)
    {
        hintMessagetext.text = hint;
        StartCoroutine(HintEnable());

        if (mushroomTransform.Length > index)
        {
            GameObject mushroom = Instantiate(mushroomPrefab, mushroomTransform[index], Quaternion.identity);
            mushroom.SetActive(true);
            var mushroomComponent = mushroom.GetComponent<Mushroom>();
            mushroomComponent.AssignButton(mushroomButton);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Got Hint");
        if (hintSet.Count < hintMessage.Length) 
        {
            hintButton.gameObject.SetActive(true); 
        }
    }

    private void OnHintButtonClicked()
    {
        for (int i = 0; i < hintMessage.Length; i++)
        {
            if (!hintSet.Contains(i)) 
            {
                ShowHint(hintMessage[i], i);
                hintSet.Add(i); 
                hintButton.gameObject.SetActive(false); 
                break; 
            }
        }
    }

    IEnumerator HintEnable()
    {
        hintPanel.SetActive(true);
        yield return new WaitForSeconds(7);
        hintPanel.SetActive(false);
    }
}
