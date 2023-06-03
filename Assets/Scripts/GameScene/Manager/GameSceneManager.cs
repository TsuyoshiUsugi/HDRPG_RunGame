using System;
using UnityEngine;
using UniRx;

/// <summary>
/// �Q�[���V�[�����Ǘ�����
/// Ready,Playing,Pose,Result�̃X�e�[�g�������A�X�e�[�g�ɉ������������s���B
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    [Header("�Q��")]
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
        ReadyStateEvent?.Invoke();
        Debug.Log("Ready!");
    }

    /// <summary>
    /// Player�̐�������
    /// </summary>
    void SetPlayer()
    {
        ControlPlayerMove(false);
        Debug.Log("Player�ݒ�");
    }

    /// <summary>
    /// true��Player�𓮂���
    /// false��Player���~
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
    /// �Q�[���v���C���̏������s��
    /// </summary>
    void PlayingState()
    {
        Debug.Log("Playing!");
        ControlPlayerMove(true);
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
