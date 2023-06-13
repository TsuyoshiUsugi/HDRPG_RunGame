using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>
/// ステージの移動処理を行う
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField] IntReactiveProperty _currentStage = new IntReactiveProperty(0);
    public IntReactiveProperty CurrentStageNum => _currentStage;
    BoolReactiveProperty _currentChoice = new BoolReactiveProperty(false);
    public BoolReactiveProperty CurrentChoice => _currentChoice;

    [SerializeField] int _stageNum = 0;
    bool _isShowConfirmBoard = false;
    bool _isFadeOutEned = false;

    public event Action<bool, int> ShowConfirmEvent;

    private async void Start()
    {
        _stageNum = WorldDataLoader.Instance.LoadedWorldDatas.Count - 1;
        await SceneLoadManager.Instance.OnStartScene();
        _isFadeOutEned = true;
    }

    /// <summary>
    /// 現在選択しているものを表示する
    /// trueで増加、falseで減少
    /// </summary>
    /// <param name="right"></param>
    public void MoveCursor(bool right) 
    {
        if (!_isFadeOutEned) return;

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
        WorldDataLoader.Instance.CurrentStageNum = _currentStage.Value;
        SceneLoadManager.Instance.LoadScene(WorldDataLoader.Instance.LoadedWorldDatas[_currentStage.Value].LoadSceneName);
    }

    /// <summary>
    /// 決定ボタンが押された時の処理
    /// </summary>
    public void OnDecideButtonCliccked()
    {
        if (!_isFadeOutEned) return;

        if (!_isShowConfirmBoard)
        {
            _isShowConfirmBoard = true;
            ShowConfirmEvent?.Invoke(true, _currentStage.Value);
        }
        else
        {
            if (!_currentChoice.Value)
            {
                ShowConfirmEvent?.Invoke(false, _currentStage.Value);
                _isShowConfirmBoard = false;
            }
            else
            {
                DecideStage();
            }
        }
    }
}
