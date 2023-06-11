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
    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(() => SceneManager.LoadScene(_nextScene));
    }
}
