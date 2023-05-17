using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// View��Model���Ȃ��N���X
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
    /// View��Model�̊e�C�x���g��o�^����
    /// </summary>
    void RegisterEvent()
    {
        _input.OnLeftButtonClicked += () => _stageSelectManager.MoveChara(false);
        _input.OnRightButtonClicked += () => _stageSelectManager.MoveChara(true);
        _input.OnMiddleButtonClicked += () => _stageSelectManager.DecideStage();

        _stageSelectManager.CurrentStageNum.Subscribe(num => _stageSelectView.CharaCursorImageMove(num));
    }
}
