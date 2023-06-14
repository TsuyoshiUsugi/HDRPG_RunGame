using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelDataLoader : MonoBehaviour
{
    [SerializeField] PlayerLevelData _playerLevelData;
    [SerializeField] int _currentLevel = 1;
    [SerializeField] int _currentlevelExp = 0;
    [SerializeField] EachLevelData _currentLevelData;
    string _getCurrentLevelKey = "CurrentLevel";
    string _getCurrentLevelExpKey = "CurrentLevelExp";

    public EachLevelData CurrentLevelData => _currentLevelData;

    // Start is called before the first frame update
    void Start()
    {
        _currentLevel = PlayerPrefs.GetInt(_getCurrentLevelKey, 1);
        _currentlevelExp = PlayerPrefs.GetInt(_getCurrentLevelExpKey, 0);
        _currentLevelData = _playerLevelData.Data[_currentLevel - 1];
    }

    public void CheckLevelUp(int exp)
    {
        if (_playerLevelData.Data[_currentLevel - 1].RequireExp <= _currentlevelExp + exp)
        {
            var restExp = _currentlevelExp + exp - _playerLevelData.Data[_currentLevel - 1].RequireExp;
            _currentLevel++;
            _currentLevelData = _playerLevelData.Data[_currentLevel - 1];
            PlayerPrefs.SetInt(_getCurrentLevelKey, _currentLevel);
            PlayerPrefs.SetInt(_getCurrentLevelExpKey, restExp);
            CheckLevelUp(restExp);
        }
        else
        {
            PlayerPrefs.SetInt(_getCurrentLevelExpKey, _currentlevelExp + exp);
        }
    }
}
