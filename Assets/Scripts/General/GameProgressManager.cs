using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : SingletonMonobehavior<GameProgressManager>
{
    [SerializeField] GameProgressData _progressData;
    Dictionary<bool, string> _loadedProgressData = new Dictionary<bool, string>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in _progressData.GameProgressDictionary.GetList())
        {
            var isClear = PlayerPrefs.GetInt(data.Key, 0);

            if (isClear != 0)
            {
                _loadedProgressData.Add(true, data.Value);
            }
            else
            {
                _loadedProgressData.Add(false, data.Value);
            }
        }
    }

    public void Clear(int num)
    {

    }
}
