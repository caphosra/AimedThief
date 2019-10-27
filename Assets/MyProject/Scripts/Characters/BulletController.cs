using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletController : ActivatableObject
{
    [SerializeField]
    private BulletStatusTable table;
    public BulletStatusTable Table { get => table; }

    Coroutine destoryMe;

    void OnEnable()
    {
        destoryMe = StartCoroutine(DestroyMe(gameObject));
    }

    void OnDisable()
    {
        StopCoroutine(destoryMe);
    }

    private IEnumerator DestroyMe(GameObject myself)
    {
        yield return new WaitForSeconds(Table.LifeTime);
        
        myself.SetActive(false);

        yield return null;
    }
}
