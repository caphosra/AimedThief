using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Cannon : ActivatableObject
{
    [SerializeField]
    private EnemyController controller;

    [SerializeField]
    private BulletStatusTable bullet;

    private const string PLAYERS_SHIP_NAME = "AllyShip";
    private GameObject playerShip;
    private ObjectPooling pool;

    void Awake()
    {
        OnActive.AddListener(OnActiveCallback);
    }

    void Update()
    {
        if(ActiveSelf)
        {
            if(playerShip.transform.position.x > transform.position.x)
            {
                var scale = transform.localScale;
                scale.x = -1f;
                transform.localScale = scale;
            }
            else
            {
                var scale = transform.localScale;
                scale.x = 1f;
                transform.localScale = scale;
            }
        }
    }

    void OnActiveCallback()
    {
        playerShip = GameObject.Find(PLAYERS_SHIP_NAME);
        pool = bullet.GetBulletPool();

        StartCoroutine(FireAndReload());
    }

    public void Fire()
    {
        if(playerShip.transform.position.y >= transform.position.y)
        {
            var obj = pool.GetObject();
            obj.transform.position = transform.position;
            var body = obj.GetComponent<Rigidbody2D>();
            var vector = (playerShip.transform.position - transform.position).normalized;
            body.velocity = vector * controller.Table.BulletSpeed;
        }
    }

    private IEnumerator FireAndReload()
    {
        while(ActiveSelf)
        {
            Fire();
            yield return new WaitForSeconds(controller.Table.BulletInterval);
        }
    }
}
