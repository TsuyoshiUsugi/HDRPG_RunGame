using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݑI������Ă��郏�[���h�̃f�[�^��ǂݎ��A�ێ�����
/// </summary>
public class WorldDataLoader : SingletonMonobehavior<WorldDataLoader>
{
    [Header("�Q��")]
    [SerializeField] WorldData _currentWorldData;
    
    List<LoadedWorldData> _loadedWorldDatas = new List<LoadedWorldData>();
    public List<LoadedWorldData> LoadedWorldDatas => _loadedWorldDatas;

    // Start is called before the first frame update
    void Start()
    {
        LoadWorldData();
    }

    /// <summary>
    /// ���ݑI������Ă��郏�[���h�̃f�[�^��ǂݎ��A���X�g�Ɋi�[
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
/// ���[�h���ꂽ���[���h�f�[�^
/// 
/// �X�e�[�W��
/// �N���A���Ă��邩(0 = ���N���A�A�P= �N���A)
/// �n�C�X�R�A
/// ���[�h����scene�̖��O������
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

