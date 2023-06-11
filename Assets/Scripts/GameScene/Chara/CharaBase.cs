using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// 敵、Player、ボスのベースとなるクラス
/// 
/// [変数]
/// ・HP
/// ・ATK
/// ・Speed
/// ・ATKRate
/// 
/// [関数]
/// ・攻撃
/// ・移動
/// ・Hit
/// ・位置修正
/// 
/// [interface]
/// ・ヒット
/// ・ポーズ
/// </summary>
public class CharaBase : MonoBehaviour, IHit, IPosable
{
    [Header("設定値")]
    [SerializeField] protected IntReactiveProperty _hp = new IntReactiveProperty(1);
    [SerializeField] protected int _atk = 1;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected float _atkRate = 1;

    int _flashTime = 2;
    float _flashDur = 0.1f;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    protected float _leftLimit = -2.5f;
    protected float _rightLimit = 2;
    protected float _restCooldownTime = 0;
    
    protected BoolReactiveProperty _isDeath = new BoolReactiveProperty(false);
    protected BoolReactiveProperty _isPose = new BoolReactiveProperty(false);

    public float AtkRate => _atkRate;
    public IntReactiveProperty Hp => _hp;
    public BoolReactiveProperty IsDeath => _isDeath;

    void Awake()
    {
        TryGetComponent(out _animator);
        TryGetComponent(out _spriteRenderer);
        _leftLimit = GameSceneManager.Instance.GetFieldInfo().leftSide;
        _rightLimit = GameSceneManager.Instance.GetFieldInfo().rightSide;
        this.UpdateAsObservable().Where(_ => _isPose.Value == false).Subscribe(_ => AutoForwardMove());
    }

    public virtual void Attack() { }

    protected virtual void AutoForwardMove() {}

    public void Hit(int damage) { }

    /// <summary>
    /// ポーズ処理
    /// </summary>
    /// <param name="isPoseing"></param>
    public void Pose(bool isPoseing)
    {
        _isPose.Value = isPoseing;
    }

    /// <summary>
    /// 設定されている領域外にでないようにする
    /// </summary>
    protected void ResetPos()
    {
        if (transform.position.x > _rightLimit) transform.position = new Vector3(_rightLimit, transform.position.y, transform.position.z);
        if (transform.position.x < _leftLimit) transform.position = new Vector3(_leftLimit, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// うごけるオブジェクトはscene開始時にゲームマネージャーに追加
    /// </summary>
    private void OnEnable()
    {
        GameSceneManager.Instance.PosableObj.Add(this);
    }

    /// <summary>
    /// 被ダメ時にオブジェクトを点滅させる
    /// </summary>
    protected async void ShowHit()
    {
        if (_spriteRenderer == null) return;

        for (int i = 0; i < _flashTime; i++)
        {
            _spriteRenderer.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur));
            if (_spriteRenderer == null) return;
            _spriteRenderer.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_flashDur));
        }
    }
}
