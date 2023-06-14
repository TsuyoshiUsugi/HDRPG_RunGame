using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serialize;

public class GameProgressData : ScriptableObject
{
    [SerializeField] GameProgressDictionary _gameProgressDictionary;
}

/// <summary>
/// �i�s�x�̃L�[�ƃV�[����������
/// </summary>
[System.Serializable]
public class GameProgressDictionary : SerializeDictonary<string, string, ProgressKeyValueData>
{
}


[System.Serializable]
public class ProgressKeyValueData : KeyAndValue<string, string>
{
    public ProgressKeyValueData(string progressKey, string sceneName) : base(progressKey, sceneName)
    {

    }
}
