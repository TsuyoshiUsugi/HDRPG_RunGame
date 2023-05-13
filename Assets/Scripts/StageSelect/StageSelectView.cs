using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Z���N�g�X�e�[�W��UI�֘A�̏������܂Ƃ߂��N���X
/// </summary>
public class StageSelectView : MonoBehaviour
{
    [SerializeField] StageData _stageData;
    [SerializeField] Image _stagePoint;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;

    // Start is called before the first frame update
    void Start()
    {
        _stagePoint.sprite = _stageData.BackGroundImage;
    }

    /// <summary>
    /// �L���������ݑI�����Ă���ꏊ��\������J�[�\�����ړ�������
    /// </summary>
    /// <param name="stageNum"></param>
    public void CharaCursorImageMove(int stageNum)
    {
        _charaImage.transform.position = _stageData.StagePointPos[stageNum];
    }

    /// <summary>
    /// ���ݑI�����Ă���ꏊ���e�L�X�g�ɕ\������
    /// </summary>
    /// <param name="stageNum"></param>
    public void ShowCurrentStageText(int stageNum)
    {
        _currentStageText.text = _stageData.StageInfo[stageNum];
    }
}
