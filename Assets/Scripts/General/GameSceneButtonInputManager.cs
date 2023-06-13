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
    [SerializeField] Button _optionBUtton;
    ButtonPressDetect _rightPressDetect;
    ButtonPressDetect _middlePressDetect;
    ButtonPressDetect _leftPressDetect;

    bool _isOptionButtonDown = false;

    public override event Action OnRightButtonClicked;
    public override event Action OnMiddleButtonClicked;
    public override event Action OnLeftButtonClicked;
    public override event Action OnOptionButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        _rightButton.gameObject.AddComponent<ButtonPressDetect>();
        _middleButton.gameObject.AddComponent<ButtonPressDetect>();
        _leftButton.gameObject.AddComponent<ButtonPressDetect>();

        _leftPressDetect = _leftButton.GetComponent<ButtonPressDetect>();
        _rightPressDetect = _rightButton.GetComponent<ButtonPressDetect>();
        _middlePressDetect = _middleButton.GetComponent<ButtonPressDetect>();

        _optionBUtton.onClick.AddListener(() =>
        {
            OnOptionButtonClicked?.Invoke();
            _isOptionButtonDown = !_isOptionButtonDown;
        });

        Observable.EveryUpdate().Select(_ => _rightPressDetect.IsButtonPressed)
            .Where(press => press == true && !_isOptionButtonDown)
            .Subscribe(_ => OnRightButtonClicked?.Invoke())
            .AddTo(this);
        
        Observable.EveryUpdate().Select(_ => _leftPressDetect.IsButtonPressed)
            .Where(press => press == true && !_isOptionButtonDown)
            .Subscribe(_ => OnLeftButtonClicked?.Invoke())
            .AddTo(this);
        
        Observable.EveryUpdate().Select(_ => _middlePressDetect.IsButtonPressed)
            .Where(press => press == true && !_isOptionButtonDown)
            .Subscribe(_ => OnMiddleButtonClicked?.Invoke())
            .AddTo(this);
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


