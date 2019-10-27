using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 1.0f;
    public float ScrollSpeed { get => scrollSpeed; }

    [SerializeField]
    private UnityEvent onGameStateChanged;
    public UnityEvent OnGameStateChanged { get => onGameStateChanged; }

    public GameState CurrentGameState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(GameState state)
    {
        if(state == CurrentGameState)
        {
            return;
        }

        CurrentGameState = state;
        OnGameStateChanged.Invoke();
    }
}

public enum GameState
{
    START,
    GAME_NOW,
    GAME_OVER,
    GO_TO_NEXTSTAGE,
}
