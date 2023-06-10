using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ž¸”sŽž(Ž€–S‚µ‚½‚Æ‚«‚É)•\Ž¦‚·‚éUI
/// </summary>
public class FailedUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _showUIs;
    [SerializeField] Image _cursor;
    Vector3 _cursorRightPos = new Vector3(144, -33, 0);
    Vector3 _cursorLeftPos = new Vector3(-140.7f, -33, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (_showUIs.Count == 0 || _showUIs == null) return;
        _showUIs.ForEach(obj => obj.SetActive(false));
    }

    public void ShowFailedUI()
    {
        _showUIs.ForEach(obj => obj.SetActive(true));
    }

    public void MoveCursor(bool right)
    {
        if (right) _cursor.rectTransform.localPosition = _cursorRightPos;
        if (!right) _cursor.rectTransform.localPosition = _cursorLeftPos;
    }
}
