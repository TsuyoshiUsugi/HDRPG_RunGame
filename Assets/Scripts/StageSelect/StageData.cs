using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�̃f�[�^
/// 
/// �X�R�A�̕ۑ��ϐ��A
/// �N���A���Ă��邩�̕ۑ��ϐ��A
/// ���[�h����X�e�[�W�̃f�[�^
/// UI�̕\���n�_
/// 
/// ������
/// </summary>
[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    [SerializeField] string _stageName = "";
    public string StageName => _stageName;

    [SerializeField] string _scoreKey = "";
    public string ScoreKey => _scoreKey;

    [SerializeField] string _isClearKey = "";
    public string IsClearKey => _isClearKey;

    [SerializeField] string _loadSceneName = "";
    public string LoadSceneName => _loadSceneName;

    [SerializeField] Vector3 _showUIPoint = Vector3.zero;
    public Vector3 ShowUIPoint => _showUIPoint;
}
