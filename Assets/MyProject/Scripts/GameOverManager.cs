using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        scoreText.text = $"Your score : {ScoreDatabase.Score:0000000}";
    }
}
