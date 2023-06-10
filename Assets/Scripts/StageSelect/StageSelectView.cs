using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セレクトステージのUI関連の処理をまとめたクラス
/// </summary>
public class StageSelectView : MonoBehaviour
{
    [SerializeField] WorldData _WorldData;
    [SerializeField] Image _backGroundImage;
    [SerializeField] Image _stagePointImage;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;
    [SerializeField] Canvas _canvas;
    string _stageText = "ステージ：";

    private void Awake()
    {
        ShowMap();
        //マネージャーが呼び忘れた時のために0で呼ぶ
        CharaCursorImageMove(0);
        ShowCurrentStageText(0);
    }

    /// <summary>
    /// キャラが現在選択している場所を表示するカーソルを移動させる
    /// </summary>
    /// <param name="stageNum"></param>
    public void CharaCursorImageMove(int stageNum)
    {
        _charaImage.transform.localPosition = _WorldData.StageDatas[stageNum].ShowUIPoint;
        _charaImage.transform.SetAsLastSibling();
        ShowCurrentStageText(stageNum);
    }

    /// <summary>
    /// 現在選択している場所をテキストに表示する
    /// </summary>
    /// <param name="stageNum"></param>
    void ShowCurrentStageText(int stageNum)
    {
        _currentStageText.text = $"{_stageText}{_WorldData.StageDatas[stageNum].StageName}";
    }

    /// <summary>
    /// 画面に背景と、ステージの画像を表示する
    /// </summary>
    public void ShowMap()
    {       
        _backGroundImage.sprite = _WorldData.BackGroundImage;

        List<Vector3> stagePoints = new List<Vector3>();

        foreach (var stageData in _WorldData.StageDatas)
        {
            stagePoints.Add(stageData.ShowUIPoint);
        }

        Debug.Log(stagePoints.Count);

        //生成し、キャンバスの子オブジェクトにした後位置をいれる
        foreach (var point in stagePoints)
        {
            var image = Instantiate(_stagePointImage);
            image.transform.SetParent(_canvas.transform);

            var rect = image.GetComponent<RectTransform>();
            rect.localPosition = point;
            rect.localScale = new Vector3(1, 1, 1);
            
        }
    }
}
