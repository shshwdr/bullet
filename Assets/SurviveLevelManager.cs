using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveLevelManager : LevelManager
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        levelName = "Survival";
        hud.updateTarget("Survive "+ levelTime+" seconds");
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime)
        {
            currentTime -= Time.timeScale * Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime);
            if (currentTime <= 0)
            {
                //win
                succeedLevel();
                //player.getDamage(1);
            }
            hud.updateTimer(currentTime);
        }
    }
}
