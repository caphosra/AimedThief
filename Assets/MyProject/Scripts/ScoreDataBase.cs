using UnityEngine.Events;

static class ScoreDatabase
{
    private static int score;
    public static int Score
    {
        get => score;
        set
        {
            if(value != score)
            {
                score = value;
                OnScoreChanged?.Invoke();
            }
        }
    }
    
    public static UnityEvent OnScoreChanged { get; private set; } = new UnityEvent();

    public static void Initialize()
    {
        score = 0;
        OnScoreChanged = new UnityEvent();
    }
}