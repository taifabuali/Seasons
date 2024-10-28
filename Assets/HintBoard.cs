using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HintBoard : MonoBehaviour
{
  
    public string[] hintMessage =new string[] { "There are a triplets mountains loo; near the biggest one under an apple tree .." ,"near the cross roads under a tree you can fid one ..","find a small valley full of red poppy.... "};

    public Text hintMessagetext;
    public GameObject hintPanel;
    private bool hintShown = false;


    public HashSet<int> hintSet = new HashSet<int>();
    public GameObject mushroomPrefab;
    public Vector3[] mushroomTransform;
    private void Start()
    {
        hintPanel.gameObject.SetActive(false);

    }
    public void ShowHint(string hint)
    {
        hintMessagetext.text = hint;
        StartCoroutine(HintEnable());
        hintShown = true;
        //CreateMushroom(hintShown);

    }
    IEnumerator HintEnable()
    {
        hintPanel.SetActive(true);

        yield return new WaitForSeconds(5);
        hintPanel.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    { 
       
            Debug.Log("got hint");
            for (int i = 0; i < hintMessage.Length; i++)
            {
                
                if(!hintSet.Contains(i))
                {
                    ShowHint(hintMessage[i]);

                    if (mushroomTransform.Length > i)
                    {
                        Instantiate(mushroomPrefab, mushroomTransform[i], Quaternion.identity);


                    }

                        hintSet.Add(i);
                   
               
                break;
            }
        }


    }
    
}
