using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 次の攻撃が可能になるまでの間、攻撃できないことを示すUIのscript
/// MiddleButtonのUIを赤くする
/// </summary>
public class AttackRateUI : MonoBehaviour
{
    [SerializeField] Image _middleButtonImage;
    Color _originalColor = Color.white;
    CancellationToken ct;

    private void Start()
    {
        ct = this.GetCancellationTokenOnDestroy();
    }

    public async void ShowAttackRate(float time)
    {
        _middleButtonImage.color = Color.red;
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: ct);
        _middleButtonImage.color = _originalColor;
    }
}
