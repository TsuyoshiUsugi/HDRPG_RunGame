using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : SingletonMonobehavior<GameProgressManager>
{
    [SerializeField] GameProgressData _progressData;
    Dictionary<string, bool> _loadedProgressData = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in _progressData.GameProgressDictionary.GetList())
        {
            var isClear = PlayerPrefs.GetInt(data.Key, 0);

            if (isClear != 0)
            {
                _loadedProgressData.Add(data.Key, true);
            }
            else
            {
                _loadedProgressData.Add(data.Key, false);
            }
        }
    }

    public void Clear(string progressName)
    {
        _loadedProgressData[progressName] = true;
        PlayerPrefs.SetInt(progressName, 1);
    }
}
