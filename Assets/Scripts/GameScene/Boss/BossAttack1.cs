using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ˆê’èŠÔ‚²‚Æ‚É‰Š‚ğƒvƒŒƒCƒ„[‚ÉŒü‚©‚Á‚Ä”­Ë‚·‚é
/// </summary>
public class BossAttack1 : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject _flame;
    [SerializeField] float _attackRate = 3;
    [SerializeField] float _restCooldownTime = 0;

    public void EnemyAttack()
    {
        _restCooldownTime += Time.deltaTime;

        if (_restCooldownTime >= _attackRate)
        {
            Instantiate(_flame, transform.position, transform.rotation);
            _restCooldownTime = 0;
        }

    }
}
