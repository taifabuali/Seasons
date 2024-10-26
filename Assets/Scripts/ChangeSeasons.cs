using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSeasons : MonoBehaviour
{
    public Manager managment;

    private float timer = 0f;
    private float changeInterval = 10f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > changeInterval)
        {
            ChangeToNextSeason();
            timer = 0f;
        }
    }

    private void ChangeToNextSeason()
    {
        // Cycle through seasons
        Manager.Season newSeason = (Manager.Season)(((int)managment.currentSeason + 1) % 4);
        managment.ChangeSeason(newSeason);
    }
}
