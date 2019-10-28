using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private List<Text> otherPlayersScoreTexts;

    private void Start()
    {
        scoreText.text = $"Your score : {ScoreDatabase.Score:0000000}";

        ScoreDatabase.RegisterScore();
        var list = ScoreDatabase.GetRanking();
        bool playerScoreFound = false;
        for(int i = 0; i < list.Count; i++)
        {
            var text = otherPlayersScoreTexts[i];
            text.text = $"{list[i]:0000000}";
            if(list[i] == ScoreDatabase.Score && !playerScoreFound)
            {
                playerScoreFound = true;
                text.text += " (Your score)";
            }
        }

        ScoreDatabase.Initialize();
    }
}
