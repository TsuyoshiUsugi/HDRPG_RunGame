using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワールドのデータ
/// ステージのデータとワールド背景の画像をもつ
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateWorldData")]
public class WorldData : ScriptableObject
{
    /// <summary> ワールド背景画像 </summary>
    [SerializeField] Sprite _backGroundImage;
    public Sprite BackGroundImage => _backGroundImage;

    /// <summary> ステージデータのリスト </summary>
    [SerializeField] List<StageData> _stageDatas;
    public List<StageData> StageDatas => _stageDatas;

    /// <summary> ステージ数 </summary>
    public int StageNum => _stageDatas.Count;
}
