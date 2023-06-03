using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵、Player、ボスのベースとなるクラス
/// </summary>
public class CharaBase : MonoBehaviour, IHit
{
    int _hp = 1;
    int _atk = 1;
    protected float _speed = 1;
    float _atkRate = 1;
    BoxCollider _range;
    public BoolReactiveProperty IsDeath = new BoolReactiveProperty();

    public void Attack()
    {

    }

    protected virtual void Move()
    {

    }

    public void Hit(int damage)
    {
        
    }
}
