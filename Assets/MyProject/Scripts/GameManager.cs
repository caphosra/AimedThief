using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StageStatusTable stageStatusTable;
    public StageStatusTable StageStatusTable => stageStatusTable;

    [SerializeField]
    private UnityEvent onGameStateChanged;
    public UnityEvent OnGameStateChanged { get => onGameStateChanged; }

    public GameState CurrentGameState { get; private set; } = GameState.START;

    private const float WAIT_FOR_NEXT_SCENE_TIME = 3f;

    // Start is called before the first frame update
    void Start()
    {
        OnGameStateChanged.AddListener(OnGameOverCallback);
        OnGameStateChanged.AddListener(() => OnStageClearCallback(StageStatusTable.NextStageSceneName));
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
        yield return new WaitForSeconds(WAIT_FOR_NEXT_SCENE_TIME);
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
            ScoreDatabase.Score += stageStatusTable.BonusScore;

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
        yield return new WaitForSeconds(WAIT_FOR_NEXT_SCENE_TIME);
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
