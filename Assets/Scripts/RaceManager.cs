using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Transform FinshLine;
    public bool raceOver = false;
    Text raceText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   void checkedFinshLine(Transform player ,string winnText)
    {
        if (player.position.z >= FinshLine.position.z)
        {
            raceOver = true;
        }
    }
}
