using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>
/// ステージの移動処理を行う
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] WorldData _currentWorldData;

    IntReactiveProperty _currentStage = new IntReactiveProperty(0);
    public IntReactiveProperty CurrentStageNum => _currentStage;
    BoolReactiveProperty _currentChoice = new BoolReactiveProperty(false);
    public BoolReactiveProperty CurrentChoice => _currentChoice;

    int _stageNum = 0;
    bool _isShowConfirmBoard = false;

    public event Action<bool> ShowConfirmEvent;

    private void Awake()
    {
        _stageNum = _currentWorldData.StageNum - 1;
    }

    /// <summary>
    /// 現在選択しているものを表示する
    /// trueで増加、falseで減少
    /// </summary>
    /// <param name="right"></param>
    public void MoveCursor(bool right) 
    {
        if (_isShowConfirmBoard)
        {
            if (right)
            {
                _currentChoice.Value = true;
            }
            else
            {

                _currentChoice.Value = false;
            }
        }
        else
        {
            //増加
            if (right)
            {
                //最大値なら
                if (_currentStage.Value == _stageNum) return;
                _currentStage.Value++;
            }
            //減少
            else
            {
                //最小値なら
                if (_currentStage.Value == 0) return;
                _currentStage.Value--;
            }
        }

    }
    
    /// <summary>
    /// 現在選択されているステージのsceneをロードする
    /// </summary>
    void DecideStage()
    {
        SceneManager.LoadScene(_currentWorldData.StageDatas[_stageNum].LoadSceneName);
    }

    /// <summary>
    /// 決定ボタンが押された時の処理
    /// </summary>
    public void OnDecideButtonCliccked()
    {
        if (!_isShowConfirmBoard)
        {
            _isShowConfirmBoard = true;
            ShowConfirmEvent?.Invoke(true);
        }
        else
        {
            if (!_currentChoice.Value)
            {
                ShowConfirmEvent?.Invoke(false);
                _isShowConfirmBoard = false;
            }
            else
            {
                DecideStage();
            }
        }
    }
}
