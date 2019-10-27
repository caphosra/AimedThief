using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;
    public float ScrollSpeed { get => scrollSpeed; }

    [SerializeField]
    private string nextStageName;
    public string NextStageName { get => nextStageName; }

    [SerializeField]
    private UnityEvent onGameStateChanged;
    public UnityEvent OnGameStateChanged { get => onGameStateChanged; }

    public GameState CurrentGameState { get; private set; } = GameState.START;

    // Start is called before the first frame update
    void Start()
    {
        OnGameStateChanged.AddListener(OnGameOverCallback);
        OnGameStateChanged.AddListener(() => OnStageClearCallback(NextStageName));
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

    private void OnGameOverCallback()
    {
        if(CurrentGameState == GameState.GAME_OVER)
        {
            StartCoroutine(GameOverTask());
        }
    }

    private bool showGameOverSceneFlag = false;
    private IEnumerator GameOverTask()
    {
        StartCoroutine(GameOverSceneLoadTask());
        yield return new WaitForSeconds(3f);
        showGameOverSceneFlag = true;
    }
    private IEnumerator GameOverSceneLoadTask()
    {
        var async = SceneManager.LoadSceneAsync("GameOver");
        async.allowSceneActivation = false;
        while(async.progress < 0.9f || !showGameOverSceneFlag)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
    }

    private void OnStageClearCallback(string nextStageName)
    {
        if(CurrentGameState == GameState.GO_TO_NEXTSTAGE)
        {
            var stage = GameObject.Find("Stage").transform;
            var camera = GameObject.Find("Main Camera").transform;
            camera.SetParent(stage);

            StartCoroutine(StageClearTask(nextStageName));
        }
    }

    private bool showNextStageSceneFlag = false;
    private IEnumerator StageClearTask(string nextStageName)
    {
        StartCoroutine(NextStageSceneLoadTask(nextStageName));
        yield return new WaitForSeconds(3f);
        showGameOverSceneFlag = true;
    }
    private IEnumerator NextStageSceneLoadTask(string nextStageName)
    {
        var async = SceneManager.LoadSceneAsync(nextStageName);
        async.allowSceneActivation = false;
        while(async.progress < 0.9f || !showNextStageSceneFlag)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
    }
}

public enum GameState
{
    START,
    GAME_NOW,
    GAME_OVER,
    GO_TO_NEXTSTAGE,
}
