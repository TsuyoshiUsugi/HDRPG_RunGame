using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 選択するときのインプットマネージャ
/// </summary>
public class SelectSceneButtonInput : InputBase
{
    [SerializeField] Button _rightButton;
    [SerializeField] Button _middleButton;
    [SerializeField] Button _leftButton;
    public override event Action OnRightButtonClicked;
    public override event Action OnMiddleButtonClicked;
    public override event Action OnLeftButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        _rightButton.onClick.AddListener(() => OnRightButtonClicked());
        _leftButton.onClick.AddListener(() => OnLeftButtonClicked());
        _middleButton.onClick.AddListener(() => OnMiddleButtonClicked());
    }
}
