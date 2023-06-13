using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// ゲームシーンを管理する
/// Ready,Playing,Pose,Resultのステートをもち、ステートに応じた処理を行う。
/// </summary>
public class GameSceneManager : SingletonMonobehavior<GameSceneManager>
{
    [Header("参照")]
    [SerializeField] Player _player;
    [SerializeField] Enemy _boss;

    [Header("設定値")]
    [SerializeField] float _leftSide = -2.5f; 
    [SerializeField] float _rightSide = 2;
    [SerializeField] float _goal = 0;

    [Header("Debug")]
    [SerializeField] List<IPosable> _posableObjs = new List<IPosable>();
    [SerializeField] ReactiveProperty<GameSceneState> _gameSceneState = new ReactiveProperty<GameSceneState>();
    [SerializeField] ReactiveProperty<NextLoadScene> _nextLoadScene = new ReactiveProperty<NextLoadScene>(NextLoadScene.SelectStageScene);

    int _score = 0;
    int _aquireExp = 0;
    bool _isClear = false;
    string _stageSelectScene = "StageSelect";
    bool _inputAcceptance = false;
    float _inputAcceptanceDelayMiliSec = 1000;

    public float Goal => _goal;
    public bool IsClear => _isClear;
    public Player Player => _player;
    public string BossName => _boss.Name;
    public ReactiveProperty<NextLoadScene> CurrentNextLoadScene => _nextLoadScene;
    public List<IPosable> PosableObj { get => _posableObjs; set => _posableObjs = value; }

    public event Action ReadyStateEvent;
    public event Action FailedResultEvent;
    public event Action BeforeBossEvent;
    public event Action BossEvent;
    public event Action<int, int> ClearResultEvent;

    private void Start()
    {
        SubscribeMethod();
        Initialize();
    }

    /// <summary>
    /// 変数の初期化処理
    /// </summary>
    void Initialize()
    {
        _score = 0;
        _aquireExp = 0;
        _gameSceneState.Value = GameSceneState.Ready;
    }

    /// <summary>
    /// ステートに合わせた関数を登録
    /// _playerの監視処理
    /// </summary>
    private void SubscribeMethod()
    {
        _gameSceneState.Where(state => state == GameSceneState.Ready).Subscribe(_ => ReadyState()).AddTo(this.gameObject);
        _gameSceneState.Where(state => state == GameSceneState.Playing).Subscribe(_ => PlayingState()).AddTo(this.gameObject);
        _gameSceneState.Where(state => state == GameSceneState.Pose).Subscribe(_ => PoseState()).AddTo(this.gameObject);
        _gameSceneState.Where(state => state == GameSceneState.Result).Subscribe(_ => ResultState()).AddTo(this.gameObject);
        _gameSceneState.Where(state => state == GameSceneState.Boss).Subscribe(_ => BossState()).AddTo(this.gameObject);
        _gameSceneState.Where(stete => stete == GameSceneState.BeforeBoss).Subscribe(_ => BeforeBoss()).AddTo(this.gameObject);

        _boss.IsDeath.Where(x => x == true).Subscribe(_ => _gameSceneState.Value = GameSceneState.Result).AddTo(this);
        _player.IsDeath.Where(x => x == true).Subscribe(_ => _gameSceneState.Value = GameSceneState.Result).AddTo(this);

        Observable.EveryUpdate().Select(_ => _player.transform.position.z)
            .Where(z => z >= _goal)
            .Take(1)
            .Subscribe(_ => _gameSceneState.Value = GameSceneState.BeforeBoss)
            .AddTo(_player.gameObject);
    }

    /// <summary>
    /// フィールドの左右の端の情報を返す
    /// </summary>
    /// <returns></returns>
    public (float leftSide, float rightSide) GetFieldInfo()
    {
        return (_leftSide, _rightSide);
    }

    /// <summary>
    /// 現在のステートを変える
    /// </summary>
    /// <param name="gameSceneState"></param>
    public void SwitchState(GameSceneState gameSceneState)
    {
        _gameSceneState.Value = gameSceneState;
    }

    /// <summary>
    /// ゲーム開始前の処理を行う
    /// </summary>
    void ReadyState()
    {
        
        SetPlayer();
        SetFrameRate();
        _boss.gameObject.SetActive(false);

        ReadyStateEvent?.Invoke();
    }

    /// <summary>
    /// フレームレートを設定する
    /// </summary>
    void SetFrameRate()
    {
        Application.targetFrameRate = 30;
    }

    /// <summary>
    /// スコアを追加する
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        _score += score;
    }

    /// <summary>
    /// 経験値を追加する
    /// </summary>
    /// <param name="exp"></param>
    public void AddExp(int exp)
    {
        _aquireExp += exp;
    }

    /// <summary>
    /// Playerの生成処理
    /// </summary>
    void SetPlayer()
    {
        ControlObjsMove(false);
    }

    /// <summary>
    /// trueでオブジェクトを動かす
    /// falseでオブジェクトを停止
    /// </summary>
    void ControlObjsMove(bool enebleMove)
    {
        if (_posableObjs == null || _posableObjs.Count == 0) return;

        _posableObjs.ForEach(obj => obj.Pose(enebleMove ? false : true));
    }

    /// <summary>
    /// ゲームプレイ中の処理を行う
    /// </summary>
    void PlayingState()
    {
        ControlObjsMove(true);
    }

    /// <summary>
    /// ポーズ中の処理を行う
    /// </summary>
    void PoseState()
    {
        ControlObjsMove(false);
    }

    /// <summary>
    /// リザルトの処理を行う
    /// この時、一瞬で次のシーンに行くのを防ぐため、
    /// イベントが終ったことと一定時間が経過したことを確認して入力受付のbool変数をtrueにする
    /// </summary>
    void ResultState()
    {
        ControlObjsMove(false);

        if (_boss.IsDeath.Value)
        {
            _isClear = true;
            ClearResultEvent?.Invoke(_score, _aquireExp);
            CallSaveData();
            WaitClearResultEvent().Forget();
        }
        else
        {
            FailedResultEvent?.Invoke();
            WaitFailedResultEvent().Forget();
        }

        async UniTask WaitClearResultEvent()
        {
            await UniTask.WaitUntil(() => ClearResultEvent != null);
            await UniTask.Delay(TimeSpan.FromMilliseconds(_inputAcceptanceDelayMiliSec));
            _inputAcceptance = true;
        }

        async UniTask WaitFailedResultEvent()
        {
            await UniTask.WaitUntil(() => FailedResultEvent != null);
            await UniTask.Delay(TimeSpan.FromMilliseconds(_inputAcceptanceDelayMiliSec));
            _inputAcceptance = true;
        }
    }

    /// <summary>
    /// 次のsceneをロードする
    /// </summary>
    public void LoadNextScene()
    {
        if (_gameSceneState.Value != GameSceneState.Result) return;
        if (!_inputAcceptance) return;
        
        if (_nextLoadScene.Value == NextLoadScene.SelectStageScene)
        {
            SceneLoadManager.Instance.LoadScene(_stageSelectScene);
        }
        else if (_nextLoadScene.Value == NextLoadScene.CurrentScene)
        {
            SceneLoadManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// リザルトシーンの時Cursorを動かす
    /// </summary>
    /// <param name="right"></param>
    public void MoveCursor(bool right)
    {
        if (_gameSceneState.Value != GameSceneState.Result) return;
        if (right) _nextLoadScene.Value = NextLoadScene.SelectStageScene;
        if (!right) _nextLoadScene.Value = NextLoadScene.CurrentScene;
    }

    /// <summary>
    /// プレイヤーが次にロードすべきscene
    /// </summary>
    public enum NextLoadScene
    {
        CurrentScene,
        SelectStageScene,
    }

    /// <summary>
    /// ボス戦前のイベント呼び出し処理
    /// </summary>
    void BeforeBoss()
    {
        BeforeBossEvent?.Invoke();
    }

    /// <summary>
    /// Bossが現れた時の処理
    /// </summary>
    void BossState()
    {
        if (_player.IsDeath.Value)
        {
            _gameSceneState.Value = GameSceneState.Result;
            return;
        }

        BossEvent?.Invoke();    
        _boss.gameObject.SetActive(true);
    }

    /// <summary>
    /// データのセーブ処理の呼び出し
    /// </summary>
    void CallSaveData()
    {
        if (WorldDataLoader.Instance == null) return;
        WorldDataLoader.Instance.UpdataStageData(_score);
    }
}

public enum GameSceneState
{
    Ready,
    Playing,
    Pose,
    BeforeBoss,
    Boss,
    Result,
}
