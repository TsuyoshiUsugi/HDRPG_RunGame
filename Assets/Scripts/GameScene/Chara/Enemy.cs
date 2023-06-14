using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

/// <summary>
/// ゲームシーンのモブエネミーのスクリプト
/// ついているIEnemyMoveコンポーネントを取得し、それに合わせた動きをする
/// </summary>
public class Enemy : CharaBase
{
    [Header("設定値")]
    [SerializeField] string _name = "";
    [SerializeField] int _score = 1;
    [SerializeField] int _exp = 1;
    [SerializeField] EnemyHPUI _healthUI;
    [SerializeField] float _activateDis = 15;
    [SerializeField] bool _isBoss = false;

    Player _player = default;
    IEnemyMove _enemyMove;
    IEnemyAttack _enemyAttack;
    string _attackSeName = "Attack";
    string _hitSeName = "Hit";
    [SerializeField] FloatReactiveProperty _dis = new FloatReactiveProperty();

    public string Name => _name;

    // Start is called before the first frame update
    protected void Start()
    {
        TryGetComponent(out _enemyMove);
        TryGetComponent(out _enemyAttack);
        TryGetComponent(out _healthUI);

        if (_healthUI) _hp.Subscribe(hp => _healthUI.ShowHp(hp)).AddTo(this);
        this.UpdateAsObservable().Where(_ => _isPose.Value == false).Subscribe(_ => Attack()).AddTo(this);

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                _dis.Value = Mathf.Abs(this.transform.position.z - GameSceneManager.Instance.Player.gameObject.transform.position.z);
                Activate();
            })
            .AddTo(this.gameObject);
        this.gameObject.SetActive(false);
    }

    void Activate()
    {
        if (_isBoss) return;
        if (_dis.Value > _activateDis) return;
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hp.Value <= 0) return;
        other.TryGetComponent(out _player);
        if (_player != null) _player.Hit(_atk);
    }

    protected override void AutoForwardMove()
    {
        if (GameSceneManager.Instance.Player.IsDeath.Value) return;
        if (_enemyMove != null) _enemyMove.EnemyMove(_speed);
        ResetPos();
    }

    public override void Attack()
    {
        _enemyAttack?.EnemyAttack();
    }

    public new async void Hit(int damage)
    {
        if (_hp.Value <= 0) return;

        _hp.Value -= damage;
        PlaySE(_hitSeName);

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
