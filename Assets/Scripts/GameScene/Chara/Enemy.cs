using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// �Q�[���V�[���̃��u�G�l�~�[�̃X�N���v�g
/// ���Ă���IEnemyMove�R���|�[�l���g���擾���A����ɍ��킹������������
/// </summary>
public class Enemy : CharaBase
{
    [Header("�ݒ�l")]
    [SerializeField] int _score = 1;
    [SerializeField] int _exp = 1;

    IEnemyMove _enemyMove;
    IEnemyAttack _enemyAttack;

    // Start is called before the first frame update
    protected void Start()
    {
        TryGetComponent(out _enemyMove);
        TryGetComponent(out _enemyAttack);
        this.UpdateAsObservable().Where(_ => _isPose.Value == false).Subscribe(_ => Attack());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hp.Value <= 0) return;

        other.TryGetComponent<Player>(out Player player);
        if (player != null) player.Hit(_atk);
    }

    protected override void AutoForwardMove()
    {
        if (GameSceneManager.Instance.Player.IsDeath.Value) return;
        if (_enemyMove != null) _enemyMove.EnemyMove(_speed);
        ResetPos();
    }

    public override void Attack()
    {
        if (_enemyAttack != null) _enemyAttack.EnemyAttack();
    }

    public new async void Hit(int damage)
    {
        if (_hp.Value <= 0) return;

        Debug.Log("Hit");

        _hp.Value -= damage;
        await ShowHit();
        Death();
    }

    protected virtual void Death()
    {
        if (_hp.Value <= 0)
        {
            _hp.Value = 0;
            GameSceneManager.Instance.AddScore(_score);
            GameSceneManager.Instance.AddExp(_exp);
            _isDeath.Value = true;
            _enemyAttack = null;
            this.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
