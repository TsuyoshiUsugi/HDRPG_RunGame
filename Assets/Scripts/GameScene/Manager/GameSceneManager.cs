using System;
using UnityEngine;
using UniRx;
using System.Collections.Generic;

/// <summary>
/// ゲームシーンを管理する
/// Ready,Playing,Pose,Resultのステートをもち、ステートに応じた処理を行う。
/// </summary>
public class GameSceneManager : SingletonMonobehavior<GameSceneManager>
{
    [Header("参照")]
    [SerializeField] Player _player;
    public Player Player => _player;
    [SerializeField] BossManager _boss;

    [Header("設定値")]
    [SerializeField] float _leftSide = -2.5f; 
    [SerializeField] float _rightSide = 2;
    [SerializeField] float _goal = 0;
    public float Goal => _goal;

    [SerializeField] List<IPosable> _posableObjs = new List<IPosable>();
    public List<IPosable> PosableObj { get => _posableObjs; set => _posableObjs = value; }

    [SerializeField] ReactiveProperty<GameSceneState> _gameSceneState = new ReactiveProperty<GameSceneState>();
    int _score = 0;
    int _aquireExp = 0;

    public event Action ReadyStateEvent;
    public event Action FailedResultEvent;
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

        _boss.IsDeath.Where(x => x == true).Subscribe(_ => _gameSceneState.Value = GameSceneState.Result);
        _player.IsDeath.Where(x => x == true).Subscribe(_ => _gameSceneState.Value = GameSceneState.Result);
        Observable.EveryUpdate().Select(_ => _player.transform.position.z)
            .Where(z => z >= _goal)
            .Subscribe(_ => _gameSceneState.Value = GameSceneState.Boss)
            .AddTo(this.gameObject);
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
        Debug.Log("Ready!");

        SetPlayer();
        SetFrameRate();
        _boss.enabled = false;

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
        Debug.Log("Player設定");
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
        Debug.Log("Playing!");
        ControlObjsMove(true);
    }

    /// <summary>
    /// ポーズ中の処理を行う
    /// </summary>
    void PoseState()
    {
        Debug.Log("Pose!");
    }

    /// <summary>
    /// リザルトの処理を行う
    /// </summary>
    void ResultState()
    {
        ControlObjsMove(false);
        if (_boss.IsDeath.Value)
        {
            Debug.Log("Clear");
            ClearResultEvent?.Invoke(_score, _aquireExp);
        }
        else
        {
            FailedResultEvent?.Invoke();
        }
    }

    /// <summary>
    /// Bossが現れた時の処理
    /// </summary>
    void BossState()
    {
        _boss.enabled = true;
    }
}

public enum GameSceneState
{
    Ready,
    Playing,
    Pose,
    Boss,
    Result,
}
