using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージのデータ
/// 
/// スコアの保存変数、
/// クリアしているかの保存変数、
/// ロードするステージのデータ
/// UIの表示地点
/// 
/// をもつ
/// </summary>
[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    [SerializeField] string _stageName = "";
    public string StageName => _stageName;

    [SerializeField] string _scoreKey = "";
    public string ScoreKey => _scoreKey;

    [SerializeField] string _isClearKey = "";
    public string IsClearKey => _isClearKey;

    [SerializeField] string _loadSceneName = "";
    public string LoadSceneName => _loadSceneName;

    [SerializeField] Vector3 _showUIPoint = Vector3.zero;
    public Vector3 ShowUIPoint => _showUIPoint;
}
