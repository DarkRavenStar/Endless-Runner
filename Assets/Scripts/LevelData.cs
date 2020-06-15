using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public float gameSpeed;
    public int scoreToExceed;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Level Data", menuName = "EndlessRunner/LevelData")]
public class LevelData : ScriptableObject
{
    public List<ScoreData> scoreDataList;
}
