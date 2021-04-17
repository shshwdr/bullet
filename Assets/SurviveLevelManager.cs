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
    protected override void timeFinished()
    {
        succeedLevel();
    }
}
