using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    [SerializeField] TMP_Text timeLabel;
    [SerializeField] TMP_Text healthLabel;
    [SerializeField] TMP_Text targetLabel;
    [SerializeField] TMP_Text scoreLabel;
    float levelTime = 10f;
    float currentTime = 0f;
    bool startTime;
    public DSPlayerController player;
    public LevelManager levelManager;

    [SerializeField] TMP_Text intervalLabel;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartLevelMove()
    {
        startTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime)
        {
            updateTimer(levelManager.currentTime);
        }
    }


    public void updateTimer(float currentTime)
    {
        if (timeLabel)
        {
            timeLabel.text = "Timer: " + currentTime;

        }
        else
        {
            //Debug.Log("Timer: " + currentTime);
        }
    }

    public void updateHealth(int currentTime)
    {
        if (healthLabel)
        {
            healthLabel.text = "Health: " + currentTime;

        }
        else
        {
            //Debug.Log("Timer: " + currentTime);
        }
    }

    public void updateScore(float currentTime)
    {
        if (scoreLabel)
        {
            scoreLabel.text = "Score: " + currentTime;

        }
        else
        {
            //Debug.Log("Timer: " + currentTime);
        }
    }

    public void updateTarget(string target)
    {
        if (targetLabel)
        {
            targetLabel.text = target;

        }
        else
        {
            //Debug.Log("Timer: " + currentTime);
        }
    }

    public void updateIntervalLevel()
    {
        if (intervalLabel) { 
            if (GameManager.Instance.success)
            {

                intervalLabel.text = "You SUCCEED in " + levelManager.levelName + " level";
            }
            else
            {
                intervalLabel.text = "You FAILED in " + levelManager.levelName + " level";

            }
    }
    }
}
