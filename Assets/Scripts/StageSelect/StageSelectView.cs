using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セレクトステージのUI関連の処理をまとめたクラス
/// </summary>
public class StageSelectView : MonoBehaviour
{
    [SerializeField] StageData _stageData;
    [SerializeField] Image _stagePoint;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;

    // Start is called before the first frame update
    void Start()
    {
        _stagePoint.sprite = _stageData.BackGroundImage;
    }

    /// <summary>
    /// キャラが現在選択している場所を表示するカーソルを移動させる
    /// </summary>
    /// <param name="stageNum"></param>
    public void CharaCursorImageMove(int stageNum)
    {
        _charaImage.transform.position = _stageData.StagePointPos[stageNum];
    }

    /// <summary>
    /// 現在選択している場所をテキストに表示する
    /// </summary>
    /// <param name="stageNum"></param>
    public void ShowCurrentStageText(int stageNum)
    {
        _currentStageText.text = _stageData.StageInfo[stageNum];
    }
}
