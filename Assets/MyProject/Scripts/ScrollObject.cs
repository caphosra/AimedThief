using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScrollObject : ActivatableObject
{
    [SerializeField]
    private bool manualStart;
    public bool ManualStart => manualStart;

    private GameManager gameManager;
    private Rigidbody2D body;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        body = GetComponent<Rigidbody2D>();
        
        OnActive.AddListener(OnActiveCallback);

        if(!ManualStart)
        {
            gameManager.OnGameStateChanged.AddListener(OnGameStartCallback);
        }
    }

    void OnGameStartCallback()
    {
        if(gameManager.CurrentGameState == GameState.GAME_NOW)
        {
            SetActive(true);
        }
    }

    private void OnActiveCallback()
    {
        body.velocity = Vector2.left * gameManager.StageStatusTable.ScrollSpeed;
    }
}
