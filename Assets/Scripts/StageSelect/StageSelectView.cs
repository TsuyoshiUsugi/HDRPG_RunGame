using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セレクトステージのUI関連の処理をまとめたクラス
/// </summary>
public class StageSelectView : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] WorldData _WorldData;
    [SerializeField] Image _backGroundImage;
    [SerializeField] Image _stagePointImage;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _confirmBoard;
    [SerializeField] Image _confirmBoardCursor;
    Vector3 _yesCursorPos = new Vector3(368, -437.09f, 0);
    Vector3 _noCursorPos = new Vector3(-394, -437.09f, 0);

    string _stageText = "ステージ：";

    private void Awake()
    {
        
        ShowMap();
        ShowConfirmBoard(false);
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

    /// <summary>
    /// 確認画面を表示する
    /// </summary>
    /// <param name="show"></param>
    public void ShowConfirmBoard(bool show)
    {
        _confirmBoard.SetActive(show);
    }

    /// <summary>
    /// 引数に合わせて確認画面のカーソルを動かす
    /// </summary>
    /// <param name="yes"></param>
    public void MoveConfirmBoardCursor(bool yes)
    {
        if (yes)
        {
            _confirmBoardCursor.rectTransform.localPosition = _yesCursorPos;
        }
        else
        {
            _confirmBoardCursor.rectTransform.localPosition = _noCursorPos;
        }
    }
}
