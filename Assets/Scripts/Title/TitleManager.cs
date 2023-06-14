using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// タイトルマネージャのスクリプト
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _optionButton;
    [SerializeField] OptionUI _optionUI;
    [SerializeField] string _nextScene;

    string _story1SceneName = "Story1";
    string _selectSceneName = "StageSelect";
    
    string _selectSe = "Select";

    // Start is called before the first frame update
    async void Start()
    {
        SetNextScene();

        _startButton.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.LoadScene(_nextScene);
            AudioManager.Instance.PlaySE(_selectSe);
        });

        _optionButton.onClick.AddListener(() =>
        {
            _optionUI.ShowOptionBoard();
            AudioManager.Instance.PlaySE(_selectSe);
        });
        _optionUI.OnBgmSliderValueChanged += num => AudioManager.Instance.SetBGMVol(num);
        _optionUI.OnSESliderValueChanged += num => AudioManager.Instance.SetSEVol(num);

        await SceneLoadManager.Instance.OnStartScene();
    }

    void SetNextScene()
    {
        if (GameProgressManager.Instance.LoadedProgressData[_story1SceneName])
        {
            _nextScene = _selectSceneName;
        }
        else
        {
            _nextScene = _story1SceneName;
        }
    }
}
