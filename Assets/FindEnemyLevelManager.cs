using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemyLevelManager : LevelManager
{
    public List<string> roomNameList;
    public List<TriggerDetector> roomBoxList;

    public int roomId;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        levelName = "Find Enemy";
        if (roomId >= roomBoxList.Count)
        {
            Debug.LogError("room id too small " + roomId + " " + roomBoxList.Count);
        }
        hud.updateTarget("Find enemy in " + roomNameList[roomId]);
        roomBoxList[roomId].shouldTrigger = true;
    }

    public void findEnemy()
    {
        succeedLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
