using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    /// <summary> �X�e�[�W�w�i�摜 </summary>
    [SerializeField] Sprite _backGroundImage;
    public Sprite BackGroundImage => _backGroundImage;

    /// <summary> �X�e�[�WUI�̐����ʒu </summary>
    [SerializeField] List<Vector3> _stagePointPos;
    public List<Vector3> StagePointPos => _stagePointPos;
    
    /// <summary> �X�e�[�W�̃e�L�X�g��� </summary>
    [SerializeField] List<string> _stageInfo;
    public List<string> StageInfo => _stageInfo;

    /// <summary> �X�e�[�W�� </summary>
    public int StageNum => _stagePointPos.Count;
}
