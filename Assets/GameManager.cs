using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode { survive,collect,killEnemy, mirror}
public class GameManager : Singleton<GameManager>
{

    public int playerHealth;
    public int currentPlayerHealth;
    public float score;
    public GameMode gameMode;
    public DSPlayerController player;
    public LevelManager currentLevel;
    public bool success;
    public bool finishedLevel;

    int successedLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);   
    }


    public void RestartLevel()
    {
        player.restart();
    }

    public void FinishInvertal()
    {
        SelectLevelAndStart();
    }


    private void OnDestroy()
    {
        Debug.Log("destory");
    }

    public void StartGame()
    {
        currentPlayerHealth = playerHealth;
        score = 0;
        successedLevel = 0;
        SelectLevelAndStart();
        finishedLevel = true;
        HUD.Instance.updateHealth(currentPlayerHealth);
        HUD.Instance.updateScore(score);
    }

    public void SelectLevelAndStart()
    {

        //if (gameMode == GameMode.survive)
        {

            gameMode = GameMode.killEnemy;
        }
        //else
        {

           // gameMode = GameMode.collect;
        }
        SceneManager.LoadScene((int)gameMode + 1);

        GameEventMessage.SendEvent("LevelStart");
        //finishedInterval();
    }

    public void finishedInterval()
    {
        finishedLevel = false;
    }


    public void StartLevelMove()
    {
        HUD.Instance.StartLevelMove();
    }

    public void SucceedLevel()
    {
        if (finishedLevel)
        {
            return;
        }
        successedLevel++;
        score += 1;
        HUD.Instance.updateScore(score);
        GameEventMessage.SendEvent("finishLevel");

        success = true;
        //SelectLevelAndStart();
        finishLevel();
    }

    public void FailedLevel()
    {
        if (finishedLevel)
        {
            return;
        }
        currentPlayerHealth -= 1;
        HUD.Instance.updateHealth(currentPlayerHealth);

        success = false;
        if (currentPlayerHealth == 0)
        {
            Debug.Log("gameover");
            GameEventMessage.SendEvent("gameover");
        }
        else
        {

            GameEventMessage.SendEvent("finishLevel");
            //SelectLevelAndStart();
            finishLevel();
        }
    }


    public virtual void finishLevel()
    {
        //yield some time
        finishedLevel = true;
        BulletHell.ProjectileManager.Instance.ClearEmitters();
        HUD.Instance.updateIntervalLevel();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
