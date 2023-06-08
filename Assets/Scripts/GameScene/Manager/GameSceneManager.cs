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
    [SerializeField] List<IPosable> _posableObjs = new List<IPosable>();
    public List<IPosable> PosableObj { get => _posableObjs; set => _posableObjs = value; }

    [SerializeField] ReactiveProperty<GameSceneState> _gameSceneState = new ReactiveProperty<GameSceneState>();
    int _score = 0;
    int _aquireExp = 0;

    public event Action ReadyStateEvent;
    public event Action ResultStateEvent;

    private void Start()
    {
        _gameSceneState.Value = GameSceneState.Ready;
        _gameSceneState.Where(state => state == GameSceneState.Ready).Subscribe(_ => ReadyState()).AddTo(this.gameObject);
        _gameSceneState.Where(state => state == GameSceneState.Playing).Subscribe(_ => PlayingState()).AddTo(this.gameObject);
        _gameSceneState.Where(state => state == GameSceneState.Pose).Subscribe(_ => PoseState()).AddTo(this.gameObject);
        _gameSceneState.Where(state => state == GameSceneState.Result).Subscribe(_ => ResultState()).AddTo(this.gameObject);

        _player.IsDeath.Where(x => x == true).Subscribe(_ => _gameSceneState.Value = GameSceneState.Result);
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
        //_player.Pose(enebleMove ? false : true);

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
        Debug.Log("Result!");
        ControlObjsMove(false);
        ResultStateEvent?.Invoke();
    }
}

public enum GameSceneState
{
    Ready,
    Playing,
    Pose,
    Result,
}
