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
    public Player Player => _player;
    [SerializeField] BossManager _boss;

    [Header("�ݒ�l")]
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
    /// �ϐ��̏���������
    /// </summary>
    void Initialize()
    {
        _score = 0;
        _aquireExp = 0;
        _gameSceneState.Value = GameSceneState.Ready;
    }

    /// <summary>
    /// �X�e�[�g�ɍ��킹���֐���o�^
    /// _player�̊Ď�����
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
    /// �t�B�[���h�̍��E�̒[�̏���Ԃ�
    /// </summary>
    /// <returns></returns>
    public (float leftSide, float rightSide) GetFieldInfo()
    {
        return (_leftSide, _rightSide);
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
        SetPlayer();
        SetFrameRate();
        _boss.enabled = false;

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
    /// �X�R�A��ǉ�����
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        _score += score;
    }

    /// <summary>
    /// �o���l��ǉ�����
    /// </summary>
    /// <param name="exp"></param>
    public void AddExp(int exp)
    {
        _aquireExp += exp;
    }

    /// <summary>
    /// Player�̐�������
    /// </summary>
    void SetPlayer()
    {
        ControlObjsMove(false);
    }

    /// <summary>
    /// true�ŃI�u�W�F�N�g�𓮂���
    /// false�ŃI�u�W�F�N�g���~
    /// </summary>
    void ControlObjsMove(bool enebleMove)
    {
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
    /// Boss�����ꂽ���̏���
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
