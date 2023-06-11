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
    Sprite _backGroundImaga;
    int _currentStageNum = 0;
    List<LoadedWorldData> _loadedWorldDatas = new List<LoadedWorldData>();

    public int CurrentStageNum { get => _currentStageNum; set { _currentStageNum = value;} }
    public Sprite BackGroundImage => _backGroundImaga;
    public List<LoadedWorldData> LoadedWorldDatas => _loadedWorldDatas;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        if (_currentWorldData == null) return;

        LoadWorldData();
    }

    /// <summary>
    /// ���ݑI������Ă��郏�[���h�̃f�[�^��ǂݎ��A���X�g�Ɋi�[
    /// </summary>
    void LoadWorldData()
    {
        _backGroundImaga = _currentWorldData.BackGroundImage;
        for (int i = 0; i < _currentWorldData.StageNum; i++)
        {
            var name = _currentWorldData.StageDatas[i].StageName;
            var isClear = PlayerPrefs.GetInt(_currentWorldData.StageDatas[i].IsClearKey, 0);
            var score = PlayerPrefs.GetInt(_currentWorldData.StageDatas[i].ScoreKey, 0);
            var loadSceneName = _currentWorldData.StageDatas[i].LoadSceneName;
            var showUIPos = _currentWorldData.StageDatas[i].ShowUIPoint;
            _loadedWorldDatas.Add(new LoadedWorldData(name, isClear, score, loadSceneName, showUIPos));
        }
    }

    /// <summary>
    /// �X�e�[�W�f�[�^���X�V����
    /// </summary>
    /// <param name="score"></param>
    public void UpdataStageData(int score)
    {
        if (_loadedWorldDatas.Count == 0) return;

        _loadedWorldDatas[_currentStageNum].UpdateData(score);

        //�ۑ�
        PlayerPrefs.SetInt(_currentWorldData.StageDatas[_currentStageNum].IsClearKey, 1);
        PlayerPrefs.SetInt(_currentWorldData.StageDatas[_currentStageNum].ScoreKey, score);
    }
}

/// <summary>
/// ���[�h���ꂽ���[���h�f�[�^
/// 
/// �X�e�[�W��
/// �N���A���Ă��邩(0 = ���N���A�A�P= �N���A)
/// �n�C�X�R�A
/// ���[�h����scene�̖��O
/// UI��\������ꏊ
/// </summary>
public struct LoadedWorldData
{
    public string StageName;
    public int IsClear;
    public int HighScore;
    public string LoadSceneName;
    public Vector3 ShowUIPos;

    public LoadedWorldData(string stageName, int isClear, int highScore, string loadSceneName, Vector3 showUIPos)
    {
        StageName = stageName;
        IsClear = isClear;
        HighScore = highScore;
        LoadSceneName = loadSceneName;
        ShowUIPos = showUIPos;
    }

    public void UpdateData(int score)
    {
        IsClear = 1;
        if (HighScore < score) HighScore = score;
    }
}

