using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameCycle 
{
    void EndGame(bool success);
    void ResetGame();
    void GetPoint();




}
public interface IGameManager
{
    void AddScore(int points);
    void CheckWinCondition();

}


