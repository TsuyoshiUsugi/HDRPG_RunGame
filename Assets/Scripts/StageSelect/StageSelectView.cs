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
    [SerializeField] Image _backGroundImage;
    [SerializeField] Image _stagePointImage;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;
    [SerializeField] Canvas _canvas;

    // Start is called before the first frame update
    void Start()
    {
        ShowMap();
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

    /// <summary>
    /// 画面に背景と、ステージの画像を表示する
    /// </summary>
    public void ShowMap()
    {
       
        _backGroundImage.sprite = _stageData.BackGroundImage;



        List<Vector3> stagePoints = _stageData.StagePointPos;

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
