using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBoard : MonoBehaviour
{
    public string hintMessage = "Look near the mountain! ";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            SummerGame.Instance.ShowHint(hintMessage);

                }
    }
}
