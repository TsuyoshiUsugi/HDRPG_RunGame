using System;
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
    [SerializeField] Image _backGroundImage;
    [SerializeField] Image _stagePointImage;
    [SerializeField] Image _charaImage;
    [SerializeField] Text _currentStageText;
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _confirmBoard;
    [SerializeField] Image _confirmBoardCursor;
    [SerializeField] Text _isClearText;
    [SerializeField] Text _ScoreText;
    [SerializeField] GameObject _optionBoard;
    [SerializeField] Slider _bgmVolslider;
    [SerializeField] Slider _seVolslider;

    Vector3 _yesCursorPos = new Vector3(368, -437.09f, 0);
    Vector3 _noCursorPos = new Vector3(-394, -437.09f, 0);

    string _stageText = "�X�e�[�W�F";
    public event Action<float> OnBgmSliderValueChanged;
    public event Action<float> OnSESliderValueChanged;

    private void Start()
    {
        _optionBoard.SetActive(false);
        _bgmVolslider.onValueChanged.AddListener(num => OnBgmSliderValueChanged(num));
        _seVolslider.onValueChanged.AddListener(num => OnSESliderValueChanged(num));

        ShowMap();
        ShowConfirmBoard(false, 0);
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
        _charaImage.transform.localPosition = WorldDataLoader.Instance.LoadedWorldDatas[stageNum].ShowUIPos;
        _charaImage.transform.SetAsLastSibling();
        ShowCurrentStageText(stageNum);
    }

    /// <summary>
    /// ���ݑI�����Ă���ꏊ���e�L�X�g�ɕ\������
    /// </summary>
    /// <param name="stageNum"></param>
    void ShowCurrentStageText(int stageNum)
    {
        _currentStageText.text = $"{_stageText}{WorldDataLoader.Instance.LoadedWorldDatas[stageNum].StageName}";
    }

    /// <summary>
    /// ��ʂɔw�i�ƁA�X�e�[�W�̉摜��\������
    /// </summary>
    public void ShowMap()
    {       
        _backGroundImage.sprite = WorldDataLoader.Instance.BackGroundImage;

        List<Vector3> stagePoints = new List<Vector3>();

        foreach (var stageData in WorldDataLoader.Instance.LoadedWorldDatas)
        {
            stagePoints.Add(stageData.ShowUIPos);
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
    /// �m�F��ʂ�\�����A����\������
    /// </summary>
    /// <param name="show"></param>
    public void ShowConfirmBoard(bool show, int stageNum)
    {
        _confirmBoard.SetActive(show);
        
        if (WorldDataLoader.Instance.LoadedWorldDatas[stageNum].IsClear == 0)
        {
            _isClearText.text = $"���N���A";
        }
        else
        {
            _isClearText.text = $"�N���A�ς�";
        }

        _ScoreText.text = $"�n�C�X�R�A�F{WorldDataLoader.Instance.LoadedWorldDatas[stageNum].HighScore}";
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

    /// <summary>
    /// �I�v�V������ʂ�\������
    /// </summary>
    /// <param name="yes"></param>
    public void ShowOptionBoard()
    {
        _optionBoard.SetActive(!_optionBoard.activeInHierarchy ? true : false);
    }
}
