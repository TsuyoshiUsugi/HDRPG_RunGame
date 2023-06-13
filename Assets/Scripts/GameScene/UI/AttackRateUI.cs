using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���̍U�����\�ɂȂ�܂ł̊ԁA�U���ł��Ȃ����Ƃ�����UI��script
/// MiddleButton��UI��Ԃ�����
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
