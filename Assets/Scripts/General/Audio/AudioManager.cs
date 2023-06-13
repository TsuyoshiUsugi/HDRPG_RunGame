using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Audioを操作するマネージャクラス
/// </summary>
public class AudioManager : SingletonMonobehavior<AudioManager>
{
    [Header("設定値")]
    [SerializeField] Dictionary<string, AudioClip> _audioDectionary;

    AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _audioManager);
        SceneManager.sceneLoaded += OnChangeScene;
    }

    /// <summary>
    /// sceneが変わった時にBGMを変える処理
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    private void OnChangeScene(Scene nextScene, LoadSceneMode mode)
    {
        
    }
}
