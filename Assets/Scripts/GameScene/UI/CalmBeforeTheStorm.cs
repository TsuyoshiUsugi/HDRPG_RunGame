using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ボス戦直前のスクリプト
/// </summary>
public class CalmBeforeTheStorm : MonoBehaviour
{
    [SerializeField] Text _calmBeforeTheStorm;
    [SerializeField] float _tweenDur = 1;
    [SerializeField] float _betweenTweenDur = 3;

    private void Start()
    {
        _calmBeforeTheStorm.color = new Color(1, 1, 1, 0);
        _calmBeforeTheStorm.gameObject.SetActive(true);
    }

    public void ShowText()
    {

    }
}
