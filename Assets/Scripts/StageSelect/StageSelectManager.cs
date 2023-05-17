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
    [SerializeField] StageData _currentStageData;
    [SerializeField] IntReactiveProperty _currentStage = new IntReactiveProperty(0);
    public IntReactiveProperty CurrentStageNum => _currentStage;
    int _stageNum = 0;

    private void Awake()
    {
        _stageNum = _currentStageData.StageNum - 1;
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
    public void DecideStage()
    {
        SceneManager.LoadScene("GameScene");
    }
}
