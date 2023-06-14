using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelData", menuName = "ScriptableObjects/CreatePlayerLevelData")]
public class PlayerLevelData : ScriptableObject
{
    [SerializeField] List<EachLevelData> _levelData= new List<EachLevelData>();
    int _maxLevel = 20;
    int _requireExpRate = 10;
    int _hpIncreaseRate = 5;
    float _speedIncreaseRate = 0.01f;
    float _attackRateIncreaseRate = 0.01f;

    public int Maxlevel => _maxLevel;
    public List<EachLevelData> Data => _levelData;

    private void Awake()
    {
        //初期値
        _levelData.Add(new EachLevelData(1, 10, 3, 1, 2, 1, new Vector3(0, -0.128f, 3.14f), new Vector3(0.29f, 0.013f, 4.42f)));

        for (int i = 1; i < _maxLevel; i++)
        {
            var level = i + 1;
            var requireExp = _requireExpRate * i;
            var hp = _levelData[i - 1].Hp + i / _hpIncreaseRate;
            var atk = _levelData[i - 1].Atk + i;
            var speed = _levelData[i - 1].Speed + _speedIncreaseRate;
            var atkRate = _levelData[i - 1].AtkRate - _attackRateIncreaseRate;
            var attackHitBoxPos = _levelData[i - 1].AttackHitBoxPos + new Vector3(0, 0, 0.1f);
            var attackHitBoxSize = _levelData[i - 1].AttackHitBoxSize + new Vector3(0, 0, 0.1f);

            _levelData.Add(new EachLevelData(level, requireExp, hp, atk, speed, atkRate, attackHitBoxPos, attackHitBoxSize));
        }
    }
}

[System.Serializable]
public struct EachLevelData
{
    public int Level;
    public int RequireExp;
    public int Hp;
    public int Atk;
    public float Speed;
    public float AtkRate;
    public Vector3 AttackHitBoxSize;
    public Vector3 AttackHitBoxPos;

    /// <summary>
    /// レベルが待つデータ
    /// </summary>
    /// <param name="level"></param>
    /// <param name="requireExp"></param>
    /// <param name="hp"></param>
    /// <param name="atk"></param>
    /// <param name="speed"></param>
    /// <param name="atkRate"></param>
    /// <param name="attackHitBoxSize"></param>
    /// <param name="attackHitBoxPos"></param>
    public EachLevelData(int level, int requireExp , int hp, int atk, float speed, float atkRate, Vector3 attackHitBoxPos, Vector3 attackHitBoxSize)
    {
        Level = level;
        RequireExp = requireExp;
        Hp = hp;
        Atk = atk;
        Speed = speed;
        AtkRate = atkRate;
        AttackHitBoxPos = attackHitBoxPos;
        AttackHitBoxSize =  attackHitBoxSize;
    }
}
