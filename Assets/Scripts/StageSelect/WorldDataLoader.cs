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

    int _currentStageNum = 0;
    public int CurrentStageNum { get => _currentStageNum; set { _currentStageNum = value;} }

    Sprite _backGroundImaga;
    public Sprite BackGroundImage => _backGroundImaga;

    List<LoadedWorldData> _loadedWorldDatas = new List<LoadedWorldData>();
    public List<LoadedWorldData> LoadedWorldDatas => _loadedWorldDatas;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
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
        if (WorldDataLoader.Instance == null) return;
        var name = _currentWorldData.StageDatas[_currentStageNum].StageName;
        var isClear = 1;
        var loadSceneName = _currentWorldData.StageDatas[_currentStageNum].LoadSceneName;
        var showUIPos = _currentWorldData.StageDatas[_currentStageNum].ShowUIPoint;

        //�ۑ�
        PlayerPrefs.SetInt(_currentWorldData.StageDatas[_currentStageNum].IsClearKey, isClear);
        PlayerPrefs.SetInt(_currentWorldData.StageDatas[_currentStageNum].ScoreKey, score);

        _loadedWorldDatas[_currentStageNum] = new LoadedWorldData(name, isClear, score, loadSceneName, showUIPos);
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
    public readonly string StageName;
    public readonly int IsClear;
    public readonly int HighScore;
    public readonly string LoadSceneName;
    public readonly Vector3 ShowUIPos;

    public LoadedWorldData(string stageName, int isClear, int highScore, string loadSceneName, Vector3 showUIPos)
    {
        StageName = stageName;
        IsClear = isClear;
        HighScore = highScore;
        LoadSceneName = loadSceneName;
        ShowUIPos = showUIPos;
    }

}

