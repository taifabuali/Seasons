using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int scoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            SummerGame.Instance.CollectMushroom(gameObject);
            Destroy(gameObject);
        }
    }
}
