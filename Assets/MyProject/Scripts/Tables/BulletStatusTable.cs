using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu]
public class BulletStatusTable : ScriptableObject
{
    [SerializeField]
    private string bulletName;
    public string BulletName { get => bulletName; }

    [SerializeField]
    private int damage;
    public int Damage { get => damage; }

    public ObjectPooling GetBulletPool()
    {
        var obj = GameObject.Find($"{bulletName}Pool");
        if(obj == null)
        {
            throw new NullReferenceException();
        }
        return obj.GetComponent<ObjectPooling>();
    }
}
