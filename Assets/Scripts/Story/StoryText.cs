using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextData", menuName = "ScriptableObjects/CreateTextData")]
public class StoryText : ScriptableObject
{
    [SerializeField, Multiline(2)] string _text = ""; 
    public string Text => _text;
}

[CreateAssetMenu(fileName = "TextDataGroup", menuName = "ScriptableObjects/CreateTextDataGroup")]
public class StoryTextGroup : ScriptableObject
{
    [SerializeField] List<StoryText> _storyTexts = new List<StoryText>();
    public List<StoryText> StoryTexts => _storyTexts;
}
