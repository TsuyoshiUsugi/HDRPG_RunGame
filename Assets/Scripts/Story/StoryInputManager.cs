using System;
using UnityEngine;
using UnityEngine.UI;

public class StoryInputManager : MonoBehaviour
{
    [SerializeField] Button _skipButton;
    [SerializeField] Button _optionButton;
    public event Action OnSkipButtonClicked;
    public event Action OnOptionButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        _skipButton.onClick.AddListener(() => OnSkipButtonClicked?.Invoke());
        _optionButton.onClick.AddListener(() => OnOptionButtonClicked?.Invoke());
    }
}
