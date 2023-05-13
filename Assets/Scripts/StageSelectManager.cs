using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�̈ړ��������s��
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    int _currentStage = 0;
    int _stageNum = 0;

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
            if (_currentStage == _stageNum) return;
            _currentStage++;
        }
        //����
        else
        {
            //�ŏ��l�Ȃ�
            if (_currentStage == 0) return;
            _currentStage--;
        }
    }
    
    /// <summary>
    /// ���ݑI������Ă���X�e�[�W��scene�����[�h����
    /// </summary>
    public void DecideStage()
    {

    }
}
