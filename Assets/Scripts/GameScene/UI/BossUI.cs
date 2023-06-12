using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Bossí‚ÌUI‚Ì•\¦‚ğ‚·‚éƒNƒ‰ƒX
/// </summary>
public class BossUI : MonoBehaviour
{
    [SerializeField] GameObject _bossUI;
    [SerializeField] Text _bossName;
    [SerializeField] Text _calmBeforeTheStorm;
    [SerializeField] float _tweenDur = 1;
    [SerializeField] float _betweenTweenDur = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetBossInfoUI();
        SetCalmBeforeTheStormText();
    }

    private void SetCalmBeforeTheStormText()
    {
        _calmBeforeTheStorm.color = new Color(0, 0, 0, 0);
        _calmBeforeTheStorm.gameObject.SetActive(true);
    }

    private void SetBossInfoUI()
    {
        _bossUI.SetActive(false);
        _bossName.text = $"Boss:{GameSceneManager.Instance.BossName}";
    }

    public void ShowBossUI()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(_calmBeforeTheStorm.DOFade(1, _tweenDur).OnComplete(() => _calmBeforeTheStorm.DOFade(0, _tweenDur)));

        sequence.OnComplete(() => _bossUI.SetActive(true));
            
    }
}
