using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��Q���̃X�N���v�g
/// </summary>
public class ObstacleBase : MonoBehaviour, IHit
{
    [SerializeField] protected int _hp = 1;
    [SerializeField] protected int _atk = 1;
    [SerializeField] protected float _speed = 1;

    public void Hit(int damage)
    {
    }

    protected void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out Player player);
        player.Hit(_atk);
    }
}