using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>
/// ステージの移動処理を行う
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField] StageData _currentStageData;
    [SerializeField] IntReactiveProperty _currentStage = new IntReactiveProperty(0);
    public IntReactiveProperty CurrentStageNum => _currentStage;
    int _stageNum = 0;

    private void Awake()
    {
        _stageNum = _currentStageData.StageNum - 1;
    }

    /// <summary>
    /// 現在選択されているステージの値を加減する
    /// trueで増加、falseで減少
    /// </summary>
    /// <param name="_dir"></param>
    public void MoveChara(bool _dir) 
    {
        //増加
        if (_dir)
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
    
    /// <summary>
    /// 現在選択されているステージのsceneをロードする
    /// </summary>
    public void DecideStage()
    {
        SceneManager.LoadScene("GameScene");
    }
}
