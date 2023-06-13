using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// �Q�[���V�[�����Ǘ�����
/// Ready,Playing,Pose,Result�̃X�e�[�g�������A�X�e�[�g�ɉ������������s���B
/// </summary>
public class GameSceneManager : SingletonMonobehavior<GameSceneManager>
{
    [Header("�Q��")]
    [SerializeField] Player _player;
    [SerializeField] Enemy _boss;

    [Header("�ݒ�l")]
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
        _boss.gameObject.SetActive(false);

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
        ControlObjsMove(true);
    }

    /// <summary>
    /// �|�[�Y���̏������s��
    /// </summary>
    void PoseState()
    {
        ControlObjsMove(false);
    }

    /// <summary>
    /// ���U���g�̏������s��
    /// ���̎��A��u�Ŏ��̃V�[���ɍs���̂�h�����߁A
    /// �C�x���g���I�������Ƃƈ�莞�Ԃ��o�߂������Ƃ��m�F���ē��͎�t��bool�ϐ���true�ɂ���
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
    /// ����scene�����[�h����
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
    /// ���U���g�V�[���̎�Cursor�𓮂���
    /// </summary>
    /// <param name="right"></param>
    public void MoveCursor(bool right)
    {
        if (_gameSceneState.Value != GameSceneState.Result) return;
        if (right) _nextLoadScene.Value = NextLoadScene.SelectStageScene;
        if (!right) _nextLoadScene.Value = NextLoadScene.CurrentScene;
    }

    /// <summary>
    /// �v���C���[�����Ƀ��[�h���ׂ�scene
    /// </summary>
    public enum NextLoadScene
    {
        CurrentScene,
        SelectStageScene,
    }

    /// <summary>
    /// �{�X��O�̃C�x���g�Ăяo������
    /// </summary>
    void BeforeBoss()
    {
        BeforeBossEvent?.Invoke();
    }

    /// <summary>
    /// Boss�����ꂽ���̏���
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
    /// �f�[�^�̃Z�[�u�����̌Ăяo��
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
