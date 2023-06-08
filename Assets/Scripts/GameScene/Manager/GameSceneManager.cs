using System;
using UnityEngine;
using UniRx;
using System.Collections.Generic;

/// <summary>
/// �Q�[���V�[�����Ǘ�����
/// Ready,Playing,Pose,Result�̃X�e�[�g�������A�X�e�[�g�ɉ������������s���B
/// </summary>
public class GameSceneManager : SingletonMonobehavior<GameSceneManager>
{
    [Header("�Q��")]
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
    /// ���݂̃X�e�[�g��ς���
    /// </summary>
    /// <param name="gameSceneState"></param>
    public void SwitchState(GameSceneState gameSceneState)
    {
        _gameSceneState.Value = gameSceneState;
    }

    /// <summary>
    /// �Q�[���J�n�O�̏������s��
    /// </summary>
    void ReadyState()
    {
        Debug.Log("Ready!");

        SetPlayer();
        SetFrameRate();

        ReadyStateEvent?.Invoke();
    }

    /// <summary>
    /// �t���[�����[�g��ݒ肷��
    /// </summary>
    void SetFrameRate()
    {
        Application.targetFrameRate = 30;
    }

    /// <summary>
    /// Player�̐�������
    /// </summary>
    void SetPlayer()
    {
        Debug.Log("Player�ݒ�");
        ControlObjsMove(false);
    }

    /// <summary>
    /// true�ŃI�u�W�F�N�g�𓮂���
    /// false�ŃI�u�W�F�N�g���~
    /// </summary>
    void ControlObjsMove(bool enebleMove)
    {
        //_player.Pose(enebleMove ? false : true);

        if (_posableObjs == null || _posableObjs.Count == 0) return;

        _posableObjs.ForEach(obj => obj.Pose(enebleMove ? false : true));
    }

    /// <summary>
    /// �Q�[���v���C���̏������s��
    /// </summary>
    void PlayingState()
    {
        Debug.Log("Playing!");
        ControlObjsMove(true);
    }

    /// <summary>
    /// �|�[�Y���̏������s��
    /// </summary>
    void PoseState()
    {
        Debug.Log("Pose!");
    }

    /// <summary>
    /// ���U���g�̏������s��
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
