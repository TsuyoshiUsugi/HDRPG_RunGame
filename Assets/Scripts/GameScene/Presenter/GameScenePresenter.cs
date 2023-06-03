using UnityEngine;

/// <summary>
/// UIとモデルの橋渡しをするゲームシーン用のPresenter
/// </summary>
public class GameScenePresenter : MonoBehaviour
{
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] StartUI _startUI;

    // Start is called before the first frame update
    void Start()
    {
        RegisterModelEvent();
        RegisterViewEvent();
    }

    /// <summary>
    /// MVPのModel部分のイベントにViewの処理を登録する処理
    /// </summary>
    void RegisterModelEvent()
    {
        _gameSceneManager.ReadyStateEvent += () => StartCoroutine(_startUI.ShowStartUI());
    }

    /// <summary>
    /// MVPのView部分のイベントにModelの処理を登録する処理
    /// </summary>
    void RegisterViewEvent()
    {
        _startUI.OnEndShowStartUI += () => _gameSceneManager.SwitchState(GameSceneState.Playing);
    }
}
