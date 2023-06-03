using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃を受けるときに使われるインターフェース
/// </summary>
public interface IHit
{
    void Hit(int damage);
}
