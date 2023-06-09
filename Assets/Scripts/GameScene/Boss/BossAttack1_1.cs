using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ˆê’èŠÔ‚²‚Æ‚É‰Š‚ğƒvƒŒƒCƒ„[‚ÉŒü‚©‚Á‚Ä”­Ë‚·‚é
/// </summary>
public class BossAttack1_1 : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject _flame;
    [SerializeField] float _dur = 3;
    [SerializeField] float _currentTime = 0;

    public void EnemyAttack()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dur)
        {
            Instantiate(_flame, transform.position, transform.rotation);
            _currentTime = 0;
        }

    }
}
