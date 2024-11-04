using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuideManager : MonoBehaviour
{
    public static GuideManager Instance;

    public GameObject guidePanel;
    public Text guideText;
    public Button guideButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            guideButton.onClick.AddListener(OnGuideButtonClicked);
            guidePanel.SetActive(false); // Hide the panel initially
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowGuide(string[] messages, float[] durations)
    {
        StartCoroutine(DisplayGuide(messages, durations));
    }

    private IEnumerator DisplayGuide(string[] messages, float[] durations)
    {
        guidePanel.SetActive(true);
        for (int i = 0; i < messages.Length; i++)
        {
            guideText.text = messages[i];
            yield return new WaitForSeconds(durations[i]);
        }
        guidePanel.SetActive(false);
        guideButton.gameObject.SetActive(false); // Hide button after guide is displayed

    }

    public void OnGuideButtonClicked()
    {
        // Check the current game and show the appropriate guide
        if (SummerGame.Instance != null && SummerGame.Instance.IsActive)
        {
            // Summer guide messages
            string[] summerMessages = {
                "Go to the blue hint board to get hints about mushroom places...",
                "You must find them all before time's up...",
                "Good Luck..!"
            };
            float[] summerMessageDurations = { 3, 4, 2 };
            ShowGuide(summerMessages, summerMessageDurations);
        }
        else if (AutumnGame.Instance != null && AutumnGame.Instance.IsActive)
        {
            // Autumn guide messages
            string[] autumnMessages = {
                "Now Robin Hood, show me your talent in archery...",
                "Make 300 points to win this game so winter comes...",
                "Good Luck..!"
            };
            float[] autumnMessageDurations = { 3, 4, 2 };
            ShowGuide(autumnMessages, autumnMessageDurations);
        }
    }
}
