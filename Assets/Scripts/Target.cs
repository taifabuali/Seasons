using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Target : MonoBehaviour
{
    public Transform[] targetPoints;
    public int[] scorePoints;
    // Start is called before the first frame update
    
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Arrow"))
        {
            float closeDistance = float.MaxValue;
            int score = 0;

            foreach (Transform targetpoint in targetPoints)
            {
               
               float distance = Vector3.Distance(collision.GetContact(0).point,targetpoint.position);
               
                
                
                if (distance < closeDistance) {
                    closeDistance = distance;
                    score = GetScoreByDistance(distance);
                }


            }


            Debug.Log("Score " + score);

            AutumnGame.Instance.GetPoint(score);
        }
    }
    int GetScoreByDistance(float distance)
    {
        if(distance <0.5f)

            return scorePoints[0];
        else if(distance <  1f)
            return scorePoints[1];
        else if (distance < 1.5f)
            return scorePoints[2];
        return scorePoints[3];


    }
}
