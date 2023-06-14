using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextDataGroup", menuName = "ScriptableObjects/CreateTextDataGroup")]
public class StoryTextGroup : ScriptableObject
{
    [SerializeField] List<StoryText> _storyTexts = new List<StoryText>();
    public List<StoryText> StoryTexts => _storyTexts;

}
