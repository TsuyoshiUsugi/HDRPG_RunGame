using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ێ����Ă���e�L�X�g�f�[�^���Ăяo���ɉ�����
/// </summary>
public class TextManager : MonoBehaviour
{
    [SerializeField] StoryTextGroup _storyTextGroup;
    [SerializeField] Text _showTextBox;
    Queue<string> _storyTextqueue = new Queue<string>();

    private void Start()
    {
        foreach (var storyText in _storyTextGroup.StoryTexts)
        {
            _storyTextqueue.Enqueue(storyText.Text);
        }
    }

    public void ShowText()
    {
        Debug.Log("Call");
        if (_storyTextqueue.Count <= 0) return; 
        _showTextBox.text = _storyTextqueue.Dequeue();
    }
}
