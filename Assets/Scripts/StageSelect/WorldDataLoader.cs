using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 現在選択されているワールドのデータを読み取り、保持する
/// </summary>
public class WorldDataLoader : SingletonMonobehavior<WorldDataLoader>
{
    [Header("参照")]
    [SerializeField] WorldData _currentWorldData;
    
    List<LoadedWorldData> _loadedWorldDatas = new List<LoadedWorldData>();
    public List<LoadedWorldData> LoadedWorldDatas => _loadedWorldDatas;

    // Start is called before the first frame update
    void Start()
    {
        LoadWorldData();
    }

    /// <summary>
    /// 現在選択されているワールドのデータを読み取り、リストに格納
    /// </summary>
    void LoadWorldData()
    {
        for (int i = 0; i < _currentWorldData.StageNum; i++)
        {
            var name = _currentWorldData.StageDatas[i].StageName;
            var isClear = PlayerPrefs.GetInt(_currentWorldData.StageDatas[i].IsClearKey, 0);
            var score = PlayerPrefs.GetInt(_currentWorldData.StageDatas[i].ScoreKey, 0);
            var loadSceneName = _currentWorldData.StageDatas[i].LoadSceneName;
            _loadedWorldDatas.Add(new LoadedWorldData(name, isClear, score, loadSceneName));
        }
    }
}

/// <summary>
/// ロードされたワールドデータ
/// 
/// ステージ名
/// クリアしているか(0 = 未クリア、１= クリア)
/// ハイスコア
/// ロードするsceneの名前をもつ
/// </summary>
public struct LoadedWorldData
{
    public readonly string StageName;
    public readonly int IsClear;
    public readonly int HighScore;
    public readonly string LoadSceneName;

    public LoadedWorldData(string stageName, int isClear, int highScore, string loadSceneName)
    {
        StageName = stageName;
        IsClear = isClear;
        HighScore = highScore;
        LoadSceneName = loadSceneName;
    }
}

