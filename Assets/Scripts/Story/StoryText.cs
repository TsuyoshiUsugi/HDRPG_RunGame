using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextData", menuName = "ScriptableObjects/CreateTextData")]
public class StoryText : ScriptableObject
{
    [SerializeField, Multiline(2)] string _text = ""; 
    public string Text => _text;
}
