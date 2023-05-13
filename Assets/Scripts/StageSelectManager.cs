using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの移動処理を行う
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    int _currentStage = 0;
    int _stageNum = 0;

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
            if (_currentStage == _stageNum) return;
            _currentStage++;
        }
        //減少
        else
        {
            //最小値なら
            if (_currentStage == 0) return;
            _currentStage--;
        }
    }
    
    /// <summary>
    /// 現在選択されているステージのsceneをロードする
    /// </summary>
    public void DecideStage()
    {

    }
}
