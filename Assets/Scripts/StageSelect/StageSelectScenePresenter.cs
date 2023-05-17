using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ViewとModelをつなぐクラス
/// </summary>
public class StageSelectScenePresenter : MonoBehaviour
{
    [SerializeField] InputBase _input;
    [SerializeField] StageSelectView _stageSelectView;
    [SerializeField] StageSelectManager _stageSelectManager;

    // Start is called before the first frame update
    void Start()
    {
        RegisterEvent();
    }

    /// <summary>
    /// ViewとModelの各イベントを登録する
    /// </summary>
    void RegisterEvent()
    {
        _input.OnLeftButtonClicked += () => _stageSelectManager.MoveChara(false);
        _input.OnRightButtonClicked += () => _stageSelectManager.MoveChara(true);
        _input.OnMiddleButtonClicked += () => _stageSelectManager.DecideStage();

        _stageSelectManager.CurrentStageNum.Subscribe(num => _stageSelectView.CharaCursorImageMove(num));
    }
}
