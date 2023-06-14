using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelData", menuName = "ScriptableObjects/CreatePlayerLevelData")]
public class PlayerLevelData : ScriptableObject
{
    [SerializeField] List<EachLevelData> _data= new List<EachLevelData>();
    int _maxlevel = 20;

    private void Awake()
    {

        _data.Add(new EachLevelData(1, 3, 1, 1, 1, new Vector3(1, 1, 1)));

        for (int i = 1; i < _maxlevel; i++)
        {
            var level = i + 1;
            var hp = _data[i - 1].Hp + 10;
            var atk = _data[i - 1].Atk + 10;
            var speed = _data[i - 1].Speed + 10;
            var atkRate = _data[i - 1].AtkRate + 10;
            var attackHitBoxSize = _data[i - 1].AttackHitBoxSize + new Vector3(0.1f, 0, 0);

            _data.Add(new EachLevelData(level, hp, atk, speed, atkRate, attackHitBoxSize));
        }
    }
}

[System.Serializable]
public struct EachLevelData
{
    public int Level;
    public int Hp;
    public int Atk;
    public int Speed;
    public int AtkRate;
    public Vector3 AttackHitBoxSize;

    public EachLevelData(int level, int hp, int atk, int speed, int atkRate, Vector3 attackHitBoxSize)
    {
        Level = level;
        Hp = hp;
        Atk = atk;
        Speed = speed;
        AtkRate = atkRate;
        AttackHitBoxSize =  attackHitBoxSize;
    }
}
