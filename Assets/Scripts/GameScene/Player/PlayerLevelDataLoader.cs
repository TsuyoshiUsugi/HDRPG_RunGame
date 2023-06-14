using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLevelDataLoader : MonoBehaviour
{
    [SerializeField] PlayerLevelData _playerLevelData;
    [SerializeField] int _currentLevel = 1;
    [SerializeField] int _currentlevelExp = 0;
    [SerializeField] EachLevelData _currentLevelData;
    [SerializeField] bool _debug = false;
    string _getCurrentLevelKey = "CurrentLevel";
    string _getCurrentLevelExpKey = "CurrentLevelExp";

    public int CurrentLevel => _currentLevel;
    public int CurrentLevelExp => _currentlevelExp;
    public PlayerLevelData PlayerLevelData => _playerLevelData;
    public EachLevelData CurrentLevelData => _currentLevelData;

    public event Action<int, int, int> OnLevelUpEvene;

    // Start is called before the first frame update
    void Awake()
    {
        if (!_debug)
        {
            _currentLevel = PlayerPrefs.GetInt(_getCurrentLevelKey, _currentLevel);
            _currentlevelExp = PlayerPrefs.GetInt(_getCurrentLevelExpKey, 0);
        }

        if (_currentLevel > _playerLevelData.Maxlevel) _currentLevel = _playerLevelData.Maxlevel;
        _currentLevelData = _playerLevelData.Data[_currentLevel - 1];

    }

    public void CheckLevelUp(int exp)
    {
        if (_currentLevel == _playerLevelData.Maxlevel) return;

        if (_playerLevelData.Data[_currentLevel].RequireExp <= _currentlevelExp + exp)
        {
            var restExp = _currentlevelExp + exp - _playerLevelData.Data[_currentLevel].RequireExp;
            _currentLevel++;
            _currentLevelData = _playerLevelData.Data[_currentLevel - 1];
            _currentlevelExp = 0;
            PlayerPrefs.SetInt(_getCurrentLevelKey, _currentLevel);
            PlayerPrefs.SetInt(_getCurrentLevelExpKey, restExp);
            CheckLevelUp(restExp);

            var nextLevelExp = _playerLevelData.Data[_currentLevel].RequireExp - _currentlevelExp;
            OnLevelUpEvene?.Invoke(_currentLevel - 1, _currentLevel, nextLevelExp);
            Debug.Log($"{_currentLevel - 1}, {_currentLevel}, {nextLevelExp}");
        }
        else
        {
            PlayerPrefs.SetInt(_getCurrentLevelExpKey, _currentlevelExp + exp);
            _currentlevelExp = _currentlevelExp + exp;
            var nextLevelExp = _playerLevelData.Data[_currentLevel].RequireExp - _currentlevelExp;
            OnLevelUpEvene?.Invoke(0, 0, nextLevelExp);
        }
    }
}
