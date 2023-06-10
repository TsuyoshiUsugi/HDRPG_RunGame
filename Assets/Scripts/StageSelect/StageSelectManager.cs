using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>
/// �X�e�[�W�̈ړ��������s��
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField] IntReactiveProperty _currentStage = new IntReactiveProperty(0);
    public IntReactiveProperty CurrentStageNum => _currentStage;
    BoolReactiveProperty _currentChoice = new BoolReactiveProperty(false);
    public BoolReactiveProperty CurrentChoice => _currentChoice;

    [SerializeField] int _stageNum = 0;
    bool _isShowConfirmBoard = false;

    public event Action<bool> ShowConfirmEvent;

    private void Awake()
    {
        _stageNum = WorldDataLoader.Instance.LoadedWorldDatas.Count - 1;
    }

    /// <summary>
    /// ���ݑI�����Ă�����̂�\������
    /// true�ő����Afalse�Ō���
    /// </summary>
    /// <param name="right"></param>
    public void MoveCursor(bool right) 
    {
        if (_isShowConfirmBoard)
        {
            if (right)
            {
                _currentChoice.Value = true;
            }
            else
            {

                _currentChoice.Value = false;
            }
        }
        else
        {
            //����
            if (right)
            {
                //�ő�l�Ȃ�
                if (_currentStage.Value == _stageNum) return;
                _currentStage.Value++;
            }
            //����
            else
            {
                //�ŏ��l�Ȃ�
                if (_currentStage.Value == 0) return;
                _currentStage.Value--;
            }
        }

    }
    
    /// <summary>
    /// ���ݑI������Ă���X�e�[�W��scene�����[�h����
    /// </summary>
    void DecideStage()
    {
        SceneManager.LoadScene(WorldDataLoader.Instance.LoadedWorldDatas[_currentStage.Value].LoadSceneName);
    }

    /// <summary>
    /// ����{�^���������ꂽ���̏���
    /// </summary>
    public void OnDecideButtonCliccked()
    {
        if (!_isShowConfirmBoard)
        {
            _isShowConfirmBoard = true;
            ShowConfirmEvent?.Invoke(true);
        }
        else
        {
            if (!_currentChoice.Value)
            {
                ShowConfirmEvent?.Invoke(false);
                _isShowConfirmBoard = false;
            }
            else
            {
                DecideStage();
            }
        }
    }
}
