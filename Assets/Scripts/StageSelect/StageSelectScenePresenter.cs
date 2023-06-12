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
        _input.OnLeftButtonClicked += () => _stageSelectManager.MoveCursor(false);
        _input.OnRightButtonClicked += () => _stageSelectManager.MoveCursor(true);
        _input.OnMiddleButtonClicked += () => _stageSelectManager.OnDecideButtonCliccked();

        _stageSelectManager.CurrentStageNum.Subscribe(num => _stageSelectView.CharaCursorImageMove(num)).AddTo(this);
        _stageSelectManager.ShowConfirmEvent += (show, stageNum) => _stageSelectView.ShowConfirmBoard(show, stageNum);
        _stageSelectManager.CurrentChoice.Subscribe(yes => _stageSelectView.MoveConfirmBoardCursor(yes)).AddTo(this);
    }
}
