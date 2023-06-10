using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���[���h�̃f�[�^
/// �X�e�[�W�̃f�[�^�ƃ��[���h�w�i�̉摜������
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateWorldData")]
public class WorldData : ScriptableObject
{
    /// <summary> ���[���h�w�i�摜 </summary>
    [SerializeField] Sprite _backGroundImage;
    public Sprite BackGroundImage => _backGroundImage;

    /// <summary> �X�e�[�W�f�[�^�̃��X�g </summary>
    [SerializeField] List<StageData> _stageDatas;
    public List<StageData> StageDatas => _stageDatas;

    /// <summary> �X�e�[�W�� </summary>
    public int StageNum => _stageDatas.Count;
}
