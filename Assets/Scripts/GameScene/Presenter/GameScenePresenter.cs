using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI‚Æƒ‚ƒfƒ‹‚Ì‹´“n‚µ‚ð‚·‚é
/// </summary>
public class GameScenePresenter : MonoBehaviour
{
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] StartUI _startUI;

    // Start is called before the first frame update
    void Start()
    {
        RegisterManagerEvent();
        RegisterViewEvent();
    }

    void RegisterManagerEvent()
    {
        _gameSceneManager.ReadyStateEvent += () => StartCoroutine(_startUI.ShowStartUI());
    }

    void RegisterViewEvent()
    {
        _startUI.OnEndShowStartUI += () => _gameSceneManager.SwitchState(GameSceneState.Playing);
    }
}
