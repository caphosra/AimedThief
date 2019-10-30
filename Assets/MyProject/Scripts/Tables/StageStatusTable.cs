using UnityEngine;

[CreateAssetMenu]
public class StageStatusTable : ScriptableObject
{
    [SerializeField]
    private string stageName;
    public string StageName => stageName;

    [SerializeField]
    private string nextStageSceneName;
    public string NextStageSceneName => nextStageSceneName;

    [SerializeField]
    private int bonusScore;
    public int BonusScore => bonusScore;

    [SerializeField]
    private float scrollSpeed;
    public float ScrollSpeed => scrollSpeed;
}