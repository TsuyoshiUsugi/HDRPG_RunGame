using UnityEngine;
using UniRx;
using System;

/// <summary>
/// UI�ƃ��f���̋��n��������Q�[���V�[���p��Presenter
/// </summary>
public class GameScenePresenter : MonoBehaviour
{
    [Header("Model�Q��")]
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] Player _player;

    [Header("View�Q��")]
    [SerializeField] StartUI _startUI;
    [SerializeField] InputBase _InputBase;
    [SerializeField] FailedUI _failedUI;
    [SerializeField] ClearUI _clearUI;
    [SerializeField] HealthUI _playerHealthUI;

    // Start is called before the first frame update
    void Start()
    {
        RegisterModelEvent();
        RegisterViewEvent();
    }

    /// <summary>
    /// MVP��Model�����̃C�x���g��View�̏�����o�^���鏈��
    /// </summary>
    void RegisterModelEvent()
    {
        _gameSceneManager.ReadyStateEvent += () => StartCoroutine(_startUI.ShowStartUI());
        _gameSceneManager.FailedResultEvent += () => _failedUI.ShowFailedUI();
        _gameSceneManager.ClearResultEvent += (score, exp) => _clearUI.ShowClearUI(score, exp);
        _gameSceneManager.Player.Hp.Subscribe(currentHp => _playerHealthUI.ShowHp(currentHp)).AddTo(this);
        _gameSceneManager.CurrentNextLoadScene.Subscribe(nextScene =>
        {
            if (nextScene == GameSceneManager.NextLoadScene.SelectStageScene)
            {
                _failedUI.MoveCursor(true);
                _clearUI.MoveCursor(true);
            }
            if (nextScene == GameSceneManager.NextLoadScene.CurrentScene)
            {
                _failedUI.MoveCursor(false);
                _clearUI.MoveCursor(false);
            }
        }).AddTo(this);
    }

    /// <summary>
    /// MVP��View�����̃C�x���g��Model�̏�����o�^���鏈��
    /// </summary>
    void RegisterViewEvent()
    {
        _startUI.OnEndShowStartUI += () => _gameSceneManager.SwitchState(GameSceneState.Playing);
        _InputBase.OnLeftButtonClicked += () => _player.LeftRightMove(true);
        _InputBase.OnRightButtonClicked += () => _player.LeftRightMove(false);

        _InputBase.OnLeftButtonClicked += () => _gameSceneManager.MoveCursor(false);
        _InputBase.OnRightButtonClicked += () => _gameSceneManager.MoveCursor(true);

        IDisposable disposable = null;

        _InputBase.OnMiddleButtonClicked += () =>
        {
            if (disposable == null)
            {
                disposable = Observable.Timer(TimeSpan.FromSeconds(_gameSceneManager.Player.AtkRate))
                    .Subscribe(_ =>
                    {
                        disposable.Dispose();
                        disposable = null;
                    }).AddTo(this);

                _player.Attack();
                _gameSceneManager.LoadNextScene();
            }
        };
    }
}
