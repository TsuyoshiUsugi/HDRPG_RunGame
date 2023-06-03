using System;
using UnityEngine;
using UniRx;

/// <summary>
/// ゲームシーンを管理する
/// Ready,Playing,Pose,Resultのステートをもち、ステートに応じた処理を行う。
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] Player _player;

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
        ReadyStateEvent?.Invoke();
        Debug.Log("Ready!");
    }

    /// <summary>
    /// Playerの生成処理
    /// </summary>
    void SetPlayer()
    {
        ControlPlayerMove(false);
        Debug.Log("Player設定");
    }

    /// <summary>
    /// trueでPlayerを動かす
    /// falseでPlayerを停止
    /// </summary>
    void ControlPlayerMove(bool enebleMove)
    {
        if (enebleMove)
        {
            _player.enabled = true;
        }
        else
        {
            _player.enabled = false;
        }
    }

    /// <summary>
    /// ゲームプレイ中の処理を行う
    /// </summary>
    void PlayingState()
    {
        Debug.Log("Playing!");
        ControlPlayerMove(true);
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
        ReadyStateEvent?.Invoke();
        Debug.Log("Result!");
    }
}

public enum GameSceneState
{
    Ready,
    Playing,
    Pose,
    Result,
}
