using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateWorldData")]
public class WorldData : ScriptableObject
{
    /// <summary> ワールド背景画像 </summary>
    [SerializeField] Sprite _backGroundImage;
    public Sprite BackGroundImage => _backGroundImage;

    /// <summary> ワールドのステージUIの生成位置 </summary>
    [SerializeField] List<Vector3> _stagePointPos;
    public List<Vector3> StagePointPos => _stagePointPos;
    
    /// <summary> ステージのテキスト情報 </summary>
    [SerializeField] List<string> _stageInfo;
    public List<string> StageInfo => _stageInfo;

    /// <summary> ステージ数 </summary>
    public int StageNum => _stagePointPos.Count;
}
