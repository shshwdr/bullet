using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectLevelManager : LevelManager
{
    protected int currentCollectValue = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        levelName = "Collect";
        currentCollectValue = 0;
        hud.updateTarget("collect " + levelTargetValue + " stars");
    }

    public void collect()
    {
        currentCollectValue++;
        if (currentCollectValue>=levelTargetValue)
        {
            succeedLevel();
        }
    }

}
