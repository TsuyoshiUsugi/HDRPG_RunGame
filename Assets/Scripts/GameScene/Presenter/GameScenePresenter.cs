using UnityEngine;
using UniRx;
using System;

/// <summary>
/// UIとモデルの橋渡しをするゲームシーン用のPresenter
/// </summary>
public class GameScenePresenter : MonoBehaviour
{
    [Header("Model参照")]
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] Player _player;

    [Header("View参照")]
    [SerializeField] StartUI _startUI;
    [SerializeField] InputBase _InputBase;
    [SerializeField] FailedUI _failedUI;
    [SerializeField] ClearUI _clearUI;
    [SerializeField] HealthUI _playerHealthUI;
    [SerializeField] BossUI _bossUI;
    [SerializeField] AttackRateUI _attackRateUI;
    [SerializeField] PlayerEffect _playerEffect;
    string _attackEffectName = "Attack";
    CameraMover _cameraMover;

    // Start is called before the first frame update
    void Start()
    {
        RegisterModelEvent();
        RegisterViewEvent();
    }

    /// <summary>
    /// MVPのModel部分のイベントにViewの処理を登録する処理
    /// </summary>
    void RegisterModelEvent()
    {
        Camera.main.gameObject.TryGetComponent(out _cameraMover);
        _gameSceneManager.Player.OnPlayerHitEvent += dur => _cameraMover.OnHitCam(dur);
        _gameSceneManager.ReadyStateEvent += async () => await _startUI.ShowStartUI();
        _gameSceneManager.FailedResultEvent += () => _failedUI.ShowFailedUI();
        _gameSceneManager.BeforeBossEvent += () => _bossUI.ShowBossUI();
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
    /// MVPのView部分のイベントにModelの処理を登録する処理
    /// </summary>
    void RegisterViewEvent()
    {
        _startUI.OnEndShowStartUI += () => _gameSceneManager.SwitchState(GameSceneState.Playing);
        _bossUI.OnEndBeforeBossEvent += () => _gameSceneManager.SwitchState(GameSceneState.Boss);

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
                _playerEffect.ShowEffect(_attackEffectName);
                _attackRateUI.ShowAttackRate((int)_gameSceneManager.Player.AtkRate * 1000);
                _gameSceneManager.LoadNextScene();
            }
        };
    }
}
