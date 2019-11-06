using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StageStatusTable stageStatusTable;
    public StageStatusTable StageStatusTable => stageStatusTable;

    [SerializeField]
    private UnityEvent onGameStateChanged;
    public UnityEvent OnGameStateChanged { get => onGameStateChanged; }

    [Header("Timelines"), SerializeField]
    private PlayableDirector onPlayerDiedTimeline;

    public GameState CurrentGameState { get; private set; } = GameState.START;

    private const float WAIT_FOR_NEXT_SCENE_TIME = 3f;

    // Start is called before the first frame update
    void Start()
    {
        var source = GetComponent<AudioSource>();
        source.clip = StageStatusTable.BGMClip;

        OnGameStateChanged.AddListener(OnGameOverCallback);
        OnGameStateChanged.AddListener(OnStageClearCallback);
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

    private AsyncOperation loadNextStageTask;

    private void OnGameOverCallback()
    {
        if(CurrentGameState == GameState.GAME_OVER)
        {
            onPlayerDiedTimeline.Play();

            StartCoroutine(LoadNextScene("GameOver"));
        }
    }

    private void OnStageClearCallback()
    {
        if(CurrentGameState == GameState.GO_TO_NEXTSTAGE)
        {
            ScoreDatabase.Score += StageStatusTable.BonusScore;

            onPlayerDiedTimeline.Play();

            var stage = GameObject.Find("Stage").transform;
            var camera = GameObject.Find("Main Camera").transform;
            camera.SetParent(stage);
            var rightCollider = GameObject.Find("RightCollider");
            rightCollider.SetActive(false);

            StartCoroutine(LoadNextScene(StageStatusTable.NextStageSceneName));
        }
    }

    bool goToNextStageFlag = false;
    private IEnumerator LoadNextScene(string stageName)
    {
        loadNextStageTask = SceneManager.LoadSceneAsync(stageName);
        loadNextStageTask.allowSceneActivation = false;
        while(loadNextStageTask.progress < 0.9f || !goToNextStageFlag)
        {
            yield return null;
        }
        loadNextStageTask.allowSceneActivation = true;
    }

    public void MoveToNextScene()
    {
        goToNextStageFlag = true;
    }
}

public enum GameState
{
    START,
    GAME_NOW,
    GAME_OVER,
    GO_TO_NEXTSTAGE,
}
