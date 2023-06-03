using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIとモデルの橋渡しをする
/// </summary>
public class GameScenePresenter : MonoBehaviour
{
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] StartUI _startUI;

    // Start is called before the first frame update
    void Start()
    {
        _gameSceneManager.ReadyStateEvent += () => StartCoroutine(nameof(StartUI.ShowStartUI));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
