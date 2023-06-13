using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// スタートボタンのスクリプト
/// </summary>
public class StartButton : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] string _nextScene;
    string _selectSe = "Select";

    // Start is called before the first frame update
    async void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.LoadScene(_nextScene);
            AudioManager.Instance.PlaySE(_selectSe);
        });
        await SceneLoadManager.Instance.OnStartScene();
    }
}
