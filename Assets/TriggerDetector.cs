using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public bool shouldTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shouldTrigger)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (GameManager.Instance.currentLevel is FindEnemyLevelManager)
                {
                    ((FindEnemyLevelManager)GameManager.Instance.currentLevel).findEnemy();
                }
            }

        }
    }
}
