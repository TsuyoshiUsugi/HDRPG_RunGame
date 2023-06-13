using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オプションUIのscript
/// </summary>
public class OptionUI : MonoBehaviour
{
    [SerializeField] GameObject _optionBoard;
    [SerializeField] Slider _bgmVolslider;
    [SerializeField] Slider _seVolslider;

    public event Action<float> OnBgmSliderValueChanged;
    public event Action<float> OnSESliderValueChanged;

    // Start is called before the first frame update
    void Start()
    {
        _optionBoard.SetActive(false);
        _bgmVolslider.onValueChanged.AddListener(num => OnBgmSliderValueChanged(num));
        _seVolslider.onValueChanged.AddListener(num => OnSESliderValueChanged(num));
    }

    /// <summary>
    /// オプション画面を表示する
    /// </summary>
    /// <param name="yes"></param>
    public void ShowOptionBoard()
    {
        _optionBoard.SetActive(!_optionBoard.activeInHierarchy ? true : false);
    }
}
