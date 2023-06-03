using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// それぞれ敵の移動処理のインターフェース
/// </summary>
public interface IEnemyMove
{
    void EnemyMove(float speed);
}
