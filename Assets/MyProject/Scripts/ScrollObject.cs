using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScrollObject : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D body;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        body = GetComponent<Rigidbody2D>();
        
        gameManager.OnGameStateChanged.AddListener(OnGameStartCallback);
    }

    void OnGameStartCallback()
    {
        if(gameManager.CurrentGameState == GameState.GAME_NOW)
        {
            body.velocity = Vector2.left * gameManager.StageStatusTable.ScrollSpeed;
        }
    }
}
