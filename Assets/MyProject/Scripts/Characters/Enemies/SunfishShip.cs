using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SunfishShip : ActivatableObject
{
    [SerializeField]
    private EnemyController controller;

    [SerializeField]
    private Rigidbody2D body;

    private Coroutine moveUpDownTask;
    private const float UP_DOWN_INTERVAL = 0.6f;

    void Awake()
    {
        OnActive.AddListener(OnActiveCallback);
    }

    private bool activated = false;
    void OnActiveCallback()
    {
        if(!activated)
        {
            moveUpDownTask = StartCoroutine(MoveUpDown());
            activated = true;
        }
    }

    private bool isRising = true;
    IEnumerator MoveUpDown()
    {
        while(true)
        {
            isRising = !isRising;

            var direction = Vector2.left + (isRising ? Vector2.up : Vector2.down) * 3f;
            body.velocity = direction.normalized * controller.Table.MovementSpeed;

            yield return new WaitForSeconds(UP_DOWN_INTERVAL);
        }
    }
}
