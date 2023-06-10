using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [SerializeField] GameObject _showUI;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _expText;

    // Start is called before the first frame update
    void Start()
    {
        if (_showUI == null) return;
        _showUI.SetActive(false);
    }

    public void ShowClearUI(int score, int exp)
    {
        _showUI.SetActive(true);
        _scoreText.text = $"スコア：{score}";
        _expText.text = $"EXP：{exp}";
    }
}
