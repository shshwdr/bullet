using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLevelManager : LevelManager
{
    public override void Start()
    {
        base.Start();
        levelName = "Collect";
        hud.updateTarget("Move to the mirrored one");
    }
    public void pair()
    {
        succeedLevel();
    }
}
