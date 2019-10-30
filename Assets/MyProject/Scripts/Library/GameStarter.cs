using System.Collections.Generic;

using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeGameState(GameState.GAME_NOW);
    }
}