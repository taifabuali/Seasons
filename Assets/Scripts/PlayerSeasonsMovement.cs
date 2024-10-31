using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class PlayerSeasonsMovement : MonoBehaviour
{
    private ThirdPersonController thirdPersonController; 

    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        UpdateMovementSpeed();
    }

    public void UpdateMovementSpeed()
    {
        switch (Manager.Instance.currentSeason)
        {
            case Manager.Season.Summer:
                thirdPersonController.MoveSpeed = 5f; 
                break;
            case Manager.Season.Autumn:
                thirdPersonController.MoveSpeed = 4.5f; 
                break;
            case Manager.Season.Winter:
                thirdPersonController.MoveSpeed = 2f; 
                break;
            case Manager.Season.Spring:
                thirdPersonController.MoveSpeed = 4f; 
                break;
        }
    }
}
