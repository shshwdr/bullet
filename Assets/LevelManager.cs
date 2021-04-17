using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelTargetValue;
    [HideInInspector] public string levelName;


    protected HUD hud;


    public  float levelTime = 10f;
    [HideInInspector] public float currentTime = 0f;
    protected bool startTime;
    [HideInInspector] public DSPlayerController player;


    public virtual void Start()
    {
        player = GameObject.Find("Player").GetComponent<DSPlayerController>();
        player.levelManager = this;
        GameManager.Instance.currentLevel = this;
        hud = HUD.Instance;
        hud.levelManager = this;
        startLevel();
    }

    public virtual void startLevel()
    {
        currentTime = levelTime;
        startTime = false;
        HUD.Instance.updateTimer(currentTime);
        BulletHell.ProjectileManager.Instance.RegisterEmitters();
    }

    public virtual void startLevelMove()
    {
        startTime = true;
        HUD.Instance.StartLevelMove();
        BulletHell.ProjectileManager.Instance.StartEmitters();
    }

    public virtual void succeedLevel()
    {
        Debug.Log("succeed level");
        startTime = false;
        GameManager.Instance.SucceedLevel();
    }
    public virtual void failedLevel()
    {
        Debug.Log("failed level");
        startTime = false;
        GameManager.Instance.FailedLevel();
    }

    protected virtual void timeFinished()
    {
        failedLevel();
    }
    void Update()
    {
        if (startTime)
        {
            currentTime -= Time.timeScale * Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime);
            if (currentTime <= 0)
            {
                //win
                timeFinished();
                //player.getDamage(1);
            }
            hud.updateTimer(currentTime);
        }
    }

}
