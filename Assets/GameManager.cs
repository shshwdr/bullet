using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode { survive,collect}
public class GameManager : Singleton<GameManager>
{

    public int playerHealth;
    public int currentPlayerHealth;
    public float score;
    public GameMode gameMode;
    public DSPlayerController player;
    public LevelManager currentLevel;
    public bool success;

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

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1f);
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

        HUD.Instance.updateHealth(playerHealth);
        HUD.Instance.updateScore(score);
    }

    public void SelectLevelAndStart()
    {

        gameMode = GameMode.survive;
        SceneManager.LoadScene((int)gameMode + 1);
    }

    public void StartLevelMove()
    {
        HUD.Instance.StartLevelMove();
    }

    public void SucceedLevel()
    {

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
        playerHealth -= 1;
        HUD.Instance.updateHealth(playerHealth);

        success = false;
        if (playerHealth == 0)
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
        BulletHell.ProjectileManager.Instance.ClearEmitters();
        HUD.Instance.updateIntervalLevel();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
