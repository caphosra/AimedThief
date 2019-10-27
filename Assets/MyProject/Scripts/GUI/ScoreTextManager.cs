using System;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
class ScoreTextManager : MonoBehaviour
{
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        ScoreDatabase.OnScoreChanged.AddListener(OnScoreChangedCallback);
    }

    private void OnDisable()
    {
        ScoreDatabase.OnScoreChanged.RemoveAllListeners();
    }

    private void OnScoreChangedCallback()
    {
        scoreText.text = string.Format("{0:0000000}", ScoreDatabase.Score);
    }
}
