using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBoard : MonoBehaviour
{
    public string hintMessage = "There are a triplets mountains look near the biggest one under an apple tree . ";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(hintMessage + "got hint");
            SummerGame.Instance.ShowHint(hintMessage);

                }
    }
}
