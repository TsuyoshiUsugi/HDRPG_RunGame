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
    [SerializeField] Image _backGroundImage;
    [SerializeField] Image _stagePointImage;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;
    [SerializeField] Canvas _canvas;

    private void Awake()
    {
        ShowMap();
        //�}�l�[�W���[���ĂіY�ꂽ���̂��߂�0�ŌĂ�
        CharaCursorImageMove(0);
    }

    /// <summary>
    /// �L���������ݑI�����Ă���ꏊ��\������J�[�\�����ړ�������
    /// </summary>
    /// <param name="stageNum"></param>
    public void CharaCursorImageMove(int stageNum)
    {
        _charaImage.transform.localPosition = _stageData.StagePointPos[stageNum];
        _charaImage.transform.SetAsLastSibling();
    }

    /// <summary>
    /// ���ݑI�����Ă���ꏊ���e�L�X�g�ɕ\������
    /// </summary>
    /// <param name="stageNum"></param>
    public void ShowCurrentStageText(int stageNum)
    {
        _currentStageText.text = _stageData.StageInfo[stageNum];
    }

    /// <summary>
    /// ��ʂɔw�i�ƁA�X�e�[�W�̉摜��\������
    /// </summary>
    public void ShowMap()
    {       
        _backGroundImage.sprite = _stageData.BackGroundImage;

        List<Vector3> stagePoints = _stageData.StagePointPos;

        Debug.Log(stagePoints.Count);

        //�������A�L�����o�X�̎q�I�u�W�F�N�g�ɂ�����ʒu�������
        foreach (var point in stagePoints)
        {
            var image = Instantiate(_stagePointImage);
            image.transform.SetParent(_canvas.transform);

            var rect = image.GetComponent<RectTransform>();
            rect.localPosition = point;
            rect.localScale = new Vector3(1, 1, 1);
            
        }
    }
}
