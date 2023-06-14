using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelData", menuName = "ScriptableObjects/CreatePlayerLevelData")]
public class PlayerLevelData : ScriptableObject
{
    public void Awake()
    {
        Debug.Log("A");
    }
}

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
