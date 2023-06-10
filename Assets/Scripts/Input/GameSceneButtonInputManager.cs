using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

/// <summary>
/// Buttonによる入力を受け付けるクラス
/// </summary>
public class GameSceneButtonInputManager : InputBase
{
    [SerializeField] Button _rightButton;
    [SerializeField] Button _middleButton;
    [SerializeField] Button _leftButton;
    ButtonPressDetect _rightPressDetect;
    ButtonPressDetect _middlePressDetect;
    ButtonPressDetect _leftPressDetect;

    bool _isAnyButtonDown = false;
    public bool IsAnyButtonDown { get => _isAnyButtonDown; set => _isAnyButtonDown = value; }

    public override event Action OnRightButtonClicked;
    public override event Action OnMiddleButtonClicked;
    public override event Action OnLeftButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        _rightButton.gameObject.AddComponent<ButtonPressDetect>();
        _middleButton.gameObject.AddComponent<ButtonPressDetect>();
        _leftButton.gameObject.AddComponent<ButtonPressDetect>();

        _leftPressDetect = _leftButton.GetComponent<ButtonPressDetect>();
        _rightPressDetect = _rightButton.GetComponent<ButtonPressDetect>();
        _middlePressDetect = _middleButton.GetComponent<ButtonPressDetect>();
    }

    private void Update()
    {
        if (_rightPressDetect.IsButtonPressed) OnRightButtonClicked?.Invoke();
        if (_leftPressDetect.IsButtonPressed) OnLeftButtonClicked?.Invoke();
        if (_middlePressDetect.IsButtonPressed) OnMiddleButtonClicked?.Invoke();
    }
}

/// <summary>
/// ボタンが押されているか判定するボタンに付けるコンポーネント
/// </summary>
public class ButtonPressDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool _isButtonPressed = false;

    public bool IsButtonPressed => _isButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isButtonPressed= false;
    }
}


