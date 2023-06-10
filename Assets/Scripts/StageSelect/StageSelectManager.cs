using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>
/// �X�e�[�W�̈ړ��������s��
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField] WorldData _currentWorldData;
    [SerializeField] IntReactiveProperty _currentStage = new IntReactiveProperty(0);
    public IntReactiveProperty CurrentStageNum => _currentStage;

    int _stageNum = 0;
    bool _isShowConfirmBoard = false;

    public event Action ShowConfirmEvent;

    private void Awake()
    {
        _stageNum = _currentWorldData.StageNum - 1;
    }

    /// <summary>
    /// ���ݑI������Ă���X�e�[�W�̒l����������
    /// true�ő����Afalse�Ō���
    /// </summary>
    /// <param name="_dir"></param>
    public void MoveChara(bool _dir) 
    {
        //����
        if (_dir)
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
    
    /// <summary>
    /// ���ݑI������Ă���X�e�[�W��scene�����[�h����
    /// </summary>
    void DecideStage()
    {
        SceneManager.LoadScene(_currentWorldData.StageDatas[_stageNum].LoadSceneName);
    }

    /// <summary>
    /// ����{�^���������ꂽ���̏���
    /// </summary>
    public void OnDecideButtonCliccked()
    {
        if (!_isShowConfirmBoard)
        {
            _isShowConfirmBoard = true;
            ShowConfirmEvent?.Invoke();
        }
        
        if (_isShowConfirmBoard)
        {
            DecideStage();
        }
    }
}
