using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] GameObject _showUI;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _expText;
    [SerializeField] Text _nextLevelExpText;
    [SerializeField] Text _levelUpText;
    [SerializeField] Image _cursor;

    Vector3 _cursorRightPos = new Vector3(144, -226.5f, 0);
    Vector3 _cursorLeftPos = new Vector3(-149.1f, -226.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (_showUI == null) return;
        _showUI.SetActive(false);
        _levelUpText.gameObject.SetActive(false);
    }

    public void ShowClearUI(int score, int exp)
    {
        _showUI.SetActive(true);
        _scoreText.text = $"�X�R�A�F{score}";
        _expText.text = $"EXP�F{exp}";
    }

    public void ShowlevelUI(int preLevel, int currentLevel, int nextLevelExp)
    {
        if (preLevel == 0)
        {
            _nextLevelExpText.text = $"���̌o���l�܂�:{nextLevelExp}";
            return;
        }

        _levelUpText.gameObject.SetActive(true);
        _levelUpText.text = $"���x���A�b�v!:{preLevel} �� {currentLevel}";
        _nextLevelExpText.text = $"���̌o���l�܂�:{nextLevelExp}";
    }

    public void MoveCursor(bool right)
    {
        if (right) _cursor.rectTransform.localPosition = _cursorRightPos;
        if (!right) _cursor.rectTransform.localPosition = _cursorLeftPos;
    }
}
