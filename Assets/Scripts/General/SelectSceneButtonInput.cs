using System;
using UniRx;
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
    [SerializeField] Button _optionButton;
    bool _isOptionButtonDown = false;
    float _throttleMiliSecTime = 100f;

    public override event Action OnRightButtonClicked;
    public override event Action OnMiddleButtonClicked;
    public override event Action OnLeftButtonClicked;
    public override event Action OnOptionButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        IObservable<Unit> observableRightButton = _rightButton
            .OnClickAsObservable()
            .Where(_ => _isOptionButtonDown == false)
            .ThrottleFirst(TimeSpan.FromMilliseconds(_throttleMiliSecTime))
            .TakeUntilDestroy(this)
            .Do(_ => OnRightButtonClicked?.Invoke());
        
        IObservable<Unit> observableLeftButton = _leftButton
            .OnClickAsObservable()
            .Where(_ => _isOptionButtonDown == false)
            .ThrottleFirst(TimeSpan.FromMilliseconds(_throttleMiliSecTime))
            .TakeUntilDestroy(this)
            .Do(_ => OnLeftButtonClicked?.Invoke());
        
        IObservable<Unit> observableMiddleButton = _middleButton
            .OnClickAsObservable()
            .Where(_ => _isOptionButtonDown == false)
            .ThrottleFirst(TimeSpan.FromMilliseconds(_throttleMiliSecTime))
            .TakeUntilDestroy(this)
            .Do(_ => OnMiddleButtonClicked?.Invoke());
        
        IObservable<Unit> observableOptionButton = _optionButton
            .OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromMilliseconds(_throttleMiliSecTime))
            .TakeUntilDestroy(this)
            .Do(_ =>
            {
                OnOptionButtonClicked?.Invoke();
                _isOptionButtonDown = !_isOptionButtonDown;
            });

        Observable.Merge(observableLeftButton, observableMiddleButton, observableRightButton, observableOptionButton)
            .ThrottleFirst(TimeSpan.FromMilliseconds(_throttleMiliSecTime))
            .TakeUntilDestroy(this)
            .Subscribe();
    }

    enum ButtonType
    {
        Left,
        Middle,
        Option,
        Right,
    }
}
