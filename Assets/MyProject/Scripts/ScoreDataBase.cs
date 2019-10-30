using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR

using UnityEditor;

#endif

static class ScoreDatabase
{
    private static int score;
    public static int Score
    {
        get => score;
        set
        {
            if (value != score)
            {
                score = value;
                OnScoreChanged?.Invoke();
            }
        }
    }

    public const int SAVED_SCORE_COUNT = 5;

    public static UnityEvent OnScoreChanged { get; private set; } = new UnityEvent();

    public static void Initialize()
    {
        score = 0;
        OnScoreChanged = new UnityEvent();
    }

    public static void RegisterScore()
    {
        var list = GetRanking();
        list.Add(Score);
        list.Sort((a, b) => b - a);
        for (int i = 0; i < SAVED_SCORE_COUNT; i++)
        {
            PlayerPrefs.SetInt($"SCORE{i}", list[i]);
        }
    }

    public static List<int> GetRanking()
    {
        var list = new List<int>();
        for (int i = 0; i < SAVED_SCORE_COUNT; i++)
        {
            var name = $"SCORE{i}";
            if (PlayerPrefs.HasKey(name))
            {
                list.Add(PlayerPrefs.GetInt(name));
            }
            else
            {
                list.Add(0);
            }
        }
        return list;
    }

#if UNITY_EDITOR
    [MenuItem("Tools/ScoreDataBase/InitializeScores")]
#endif
    public static void InitializeAllScore()
    {
        for (int i = 0; i < SAVED_SCORE_COUNT; i++)
        {
            PlayerPrefs.SetInt($"SCORE{i}", 0);
        }
    }
}