using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Buttonによる入力を受け付けるクラス
/// </summary>
public class ButtonInputManager : InputBase
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
        _middleButton.onClick.AddListener(() => OnMiddleButtonClicked());   
        _leftButton.onClick.AddListener(() => OnRightButtonClicked());   
    }    
}
