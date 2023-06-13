using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ÿ‚ÌUŒ‚‚ª‰Â”\‚É‚È‚é‚Ü‚Å‚ÌŠÔAUŒ‚‚Å‚«‚È‚¢‚±‚Æ‚ğ¦‚·UI‚Ìscript
/// MiddleButton‚ÌUI‚ğÔ‚­‚·‚é
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
