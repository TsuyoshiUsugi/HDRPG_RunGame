using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
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

    public async UniTask ShowAttackRate(int time)
    {
        _middleButtonImage.color = Color.red;
        await UniTask.Delay(time);
        _middleButtonImage.color = _originalColor;
    }
}
