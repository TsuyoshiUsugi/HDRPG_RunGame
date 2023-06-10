using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Z���N�g�X�e�[�W��UI�֘A�̏������܂Ƃ߂��N���X
/// </summary>
public class StageSelectView : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] WorldData _WorldData;
    [SerializeField] Image _backGroundImage;
    [SerializeField] Image _stagePointImage;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _confirmBoard;
    [SerializeField] Image _confirmBoardCursor;
    Vector3 _yesCursorPos = new Vector3(368, -437.09f, 0);
    Vector3 _noCursorPos = new Vector3(-394, -437.09f, 0);

    string _stageText = "�X�e�[�W�F";

    private void Awake()
    {
        
        ShowMap();
        ShowConfirmBoard(false);
        //�}�l�[�W���[���ĂіY�ꂽ���̂��߂�0�ŌĂ�
        CharaCursorImageMove(0);
        ShowCurrentStageText(0);
    }

    /// <summary>
    /// �L���������ݑI�����Ă���ꏊ��\������J�[�\�����ړ�������
    /// </summary>
    /// <param name="stageNum"></param>
    public void CharaCursorImageMove(int stageNum)
    {
        _charaImage.transform.localPosition = _WorldData.StageDatas[stageNum].ShowUIPoint;
        _charaImage.transform.SetAsLastSibling();
        ShowCurrentStageText(stageNum);
    }

    /// <summary>
    /// ���ݑI�����Ă���ꏊ���e�L�X�g�ɕ\������
    /// </summary>
    /// <param name="stageNum"></param>
    void ShowCurrentStageText(int stageNum)
    {
        _currentStageText.text = $"{_stageText}{_WorldData.StageDatas[stageNum].StageName}";
    }

    /// <summary>
    /// ��ʂɔw�i�ƁA�X�e�[�W�̉摜��\������
    /// </summary>
    public void ShowMap()
    {       
        _backGroundImage.sprite = _WorldData.BackGroundImage;

        List<Vector3> stagePoints = new List<Vector3>();

        foreach (var stageData in _WorldData.StageDatas)
        {
            stagePoints.Add(stageData.ShowUIPoint);
        }

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

    /// <summary>
    /// �m�F��ʂ�\������
    /// </summary>
    /// <param name="show"></param>
    public void ShowConfirmBoard(bool show)
    {
        _confirmBoard.SetActive(show);
    }

    /// <summary>
    /// �����ɍ��킹�Ċm�F��ʂ̃J�[�\���𓮂���
    /// </summary>
    /// <param name="yes"></param>
    public void MoveConfirmBoardCursor(bool yes)
    {
        if (yes)
        {
            _confirmBoardCursor.rectTransform.localPosition = _yesCursorPos;
        }
        else
        {
            _confirmBoardCursor.rectTransform.localPosition = _noCursorPos;
        }
    }
}
