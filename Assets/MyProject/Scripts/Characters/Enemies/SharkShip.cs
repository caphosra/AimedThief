using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SharkShip : ActivatableObject
{
    [SerializeField]
    private EnemyController controller;

    [SerializeField]
    private Rigidbody2D body;

    void Awake()
    {
        OnActive += OnActiveCallback;
    }

    void OnActiveCallback()
    {
        body.velocity = Vector2.left * controller.Table.MovementSpeed;
    }
}
