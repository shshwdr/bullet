using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DSEnemyController : HPCharacterController
{
    enum EnemyType { melee, range };
    [SerializeField] float distanceToPlayer = 10f;
    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float bulletSpeed = 10f;


    [SerializeField] EnemyType enemyType;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            Debug.Log("enemy get hit");
            getDamage(1);
        }
    }

    protected override void Die()
    {
        base.Die();
        //Destroy(gameObject);
        GetComponent<BulletHell.ProjectileEmitterBase>().isStopped = true;
    }

    // Update is called once per frame
    //void Update()
    //{
    //switch (enemyType)
    //{
    //    case EnemyType.melee:
    //        var dir = DSPlayerController.Instance.transform.position - transform.position;
    //        dir = new Vector3(dir.x, dir.y, 0).normalized;
    //        dir *= walkSpeed;
    //        break;
    //    default:
    //        break;
    //}
    // }
}
