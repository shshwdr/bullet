using DG.Tweening;
using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DSPlayerController : HPCharacterController
{
    [SerializeField] float moveDistance = 2f;
    [SerializeField] float hitBackDistance = 2f;
    [SerializeField] float bulletForce = 1000f;
    [SerializeField] float moveTime = 1f;
    [SerializeField] float slowTimeTo = 1f;
    [SerializeField] Ease easeType;
    Rigidbody2D rb; 
    Sequence sequence;

    public bool isMirrorPlayer;

    public Transform arrow;

    Vector3 originPosition;
    [HideInInspector] public LevelManager levelManager;
    // Start is called before the first frame update
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originPosition = transform.position;
        if (!isMirrorPlayer)
        {

            HUD.Instance.player = this;
            GameManager.Instance.player = this;
        }
    }
    protected override void Die()
    {
        base.Die();

        Time.timeScale = 0;
        sequence.Kill();
        clearVelocity();

        GameManager.Instance.FailedLevel();

        //GameEventMessage.SendEvent("gameover");
        //Destroy(gameObject);
        //GetComponent<BulletHell.ProjectileEmitterBase>().isStopped = true;
    }

    public void restart()
    {
        //sequence.Kill();
        //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

        BulletHell.ProjectileManager.Instance.clearAllProjectiles();
        transform.position = originPosition;
        Time.timeScale = 1;
        resurrect();

    }

    public void resurrect()
    {
        hp = 1;
        isDead = false;
    }
    public Vector3 getMouseDirection()
    {
        if (isMirrorPlayer)
        {
            return -GameManager.Instance.player.getMouseDirection();
        }
        var target = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(target);
        var dir = (target - transform.position);
        dir = new Vector3(dir.x, dir.y, 0).normalized;
        return dir;
    }

    void clearVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        rb.Sleep();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead || GameManager.Instance. finishedLevel)
        {
            return;
        }
        arrow.up = getMouseDirection();

        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            sequence.Kill();
            clearVelocity();


            var dir = getMouseDirection() * moveDistance;

            rb.AddForce(dir, ForceMode2D.Impulse);
            // DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, moveTime).SetUpdate(true);
            StartCoroutine(slowDown());
            // The shortcuts way
            //﻿﻿﻿﻿﻿﻿﻿﻿transform.DOMove(new Vector3(2,2,2), 1);
            // The generic way
            levelManager.startLevelMove();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = 1;
            sequence.Kill();
            clearVelocity();
            //add back force?
            var bullet = PoolsManager.Instance.bulletManager.getItem();
            bullet.SetActive(true);
            bullet.transform.position = transform.position;

            var dir = getMouseDirection() * bulletForce;
            bullet.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);


            var hitdir = -getMouseDirection() * hitBackDistance;

            rb.AddForce(hitdir, ForceMode2D.Impulse);

            StartCoroutine(slowDown());
            levelManager.startLevelMove();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            sequence.Kill();
            Time.timeScale = 1;
        }
    }
    IEnumerator slowDown()
    {
        yield return new WaitForSeconds(0.1f);
        sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => Time.timeScale, x => Time.timeScale = x, slowTimeTo, moveTime).SetEase(easeType).SetUpdate(true));
    }
    private void LateUpdate()
    {
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameManager.Instance.currentLevel is PairLevelManager)
        {
            ((PairLevelManager)GameManager.Instance.currentLevel).pair();
        }
    }
}
