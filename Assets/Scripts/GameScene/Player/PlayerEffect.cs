using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの攻撃エフェクトを表示するクラス
/// </summary>
public class PlayerEffect : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        TryGetComponent(out _animator);
    }

    /// <summary>
    /// 引数に指定した名前のアニメーターのboolをtrueにする
    /// </summary>
    /// <param name="effectName"></param>
    public async void ShowEffect(string effectName)
    {
        _animator.SetBool(effectName, true);
        await UniTask.Delay(1);
        _animator.SetBool(effectName, false);
    }
}
