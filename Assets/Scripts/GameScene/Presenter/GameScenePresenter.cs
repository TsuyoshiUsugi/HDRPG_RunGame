using UnityEngine;

/// <summary>
/// UI�ƃ��f���̋��n��������Q�[���V�[���p��Presenter
/// </summary>
public class GameScenePresenter : MonoBehaviour
{
    [Header("Model�Q��")]
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] Player _player;

    [Header("View�Q��")]
    [SerializeField] StartUI _startUI;
    [SerializeField] InputBase _InputBase;

    // Start is called before the first frame update
    void Start()
    {
        RegisterModelEvent();
        RegisterViewEvent();
    }

    /// <summary>
    /// MVP��Model�����̃C�x���g��View�̏�����o�^���鏈��
    /// </summary>
    void RegisterModelEvent()
    {
        _gameSceneManager.ReadyStateEvent += () => StartCoroutine(_startUI.ShowStartUI());
    }

    /// <summary>
    /// MVP��View�����̃C�x���g��Model�̏�����o�^���鏈��
    /// </summary>
    void RegisterViewEvent()
    {
        _startUI.OnEndShowStartUI += () => _gameSceneManager.SwitchState(GameSceneState.Playing);
        _InputBase.OnLeftButtonClicked += () => _player.LeftRightMove(true);
        _InputBase.OnRightButtonClicked += () => _player.LeftRightMove(false);
    }
}
