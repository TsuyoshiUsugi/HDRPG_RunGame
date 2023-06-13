using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 障害物のスクリプト
/// </summary>
public class ObstacleBase : MonoBehaviour, IHit
{
    [SerializeField] protected int _hp = 1;
    [SerializeField] protected int _atk = 1;
    [SerializeField] protected float _speed = 1;

    public void Hit(int damage)
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Player player);
        if (player)
        {
            player.Hit(_atk);
            Destroy(this.gameObject);
        }
    }
}
