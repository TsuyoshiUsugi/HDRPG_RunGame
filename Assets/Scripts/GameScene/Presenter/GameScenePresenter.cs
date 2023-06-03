using UnityEngine;

/// <summary>
/// UIとモデルの橋渡しをするゲームシーン用のPresenter
/// </summary>
public class GameScenePresenter : MonoBehaviour
{
    [Header("Model参照")]
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] Player _player;

    [Header("View参照")]
    [SerializeField] StartUI _startUI;
    [SerializeField] InputBase _InputBase;

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
        _InputBase.OnLeftButtonClicked += () => _player.LeftRightMove(true);
        _InputBase.OnRightButtonClicked += () => _player.LeftRightMove(false);
    }
}
