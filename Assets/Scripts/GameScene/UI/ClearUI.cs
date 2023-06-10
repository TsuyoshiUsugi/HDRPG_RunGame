using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] GameObject _showUI;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _expText;
    [SerializeField] Image _cursor;

    Vector2 _cursorRightPos = new Vector3();
    Vector2 _cursorLeftPos = new Vector3();

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

    public void MoveCursor(bool right)
    {
        if (right) _cursor.rectTransform.localPosition = _cursorRightPos;
        if (!right) _cursor.rectTransform.localPosition = _cursorLeftPos;
    }
}
