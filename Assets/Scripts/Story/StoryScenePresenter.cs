using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScenePresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] StoryTimeLineManager _storyTimeLineManager;

    [Header("View")]
    [SerializeField] StoryInputManager _storyInputManager;
    [SerializeField] OptionUI _optionUI;
    string _skipButtonSeName = "Select"; 

    // Start is called before the first frame update
    void Start()
    {
        //model

        //view
        _storyInputManager.OnSkipButtonClicked += () => _storyTimeLineManager.Skip();
        _storyInputManager.OnOptionButtonClicked += () => _optionUI.ShowOptionBoard();
        _storyInputManager.OnOptionButtonClicked += () => _storyTimeLineManager.Pose();
        _storyInputManager.OnSkipButtonClicked += () => AudioManager.Instance.PlaySE(_skipButtonSeName);
        _storyInputManager.OnOptionButtonClicked += () => AudioManager.Instance.PlaySE(_skipButtonSeName);

        _optionUI.OnBgmSliderValueChanged += num => AudioManager.Instance.SetBGMVol(num);
        _optionUI.OnSESliderValueChanged += num => AudioManager.Instance.SetSEVol(num);
    }
}
