using Serialize;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

/// <summary>
/// Audioを操作するマネージャクラス
/// </summary>
public class AudioManager : SingletonMonobehavior<AudioManager>
{
    [Header("設定値")]
    [SerializeField] SceneAudioDictionary _audioDectionary;
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _audioSource);
        SceneManager.sceneLoaded += OnChangeScene;

        if (_audioSource && _audioSource.isPlaying != false)
        {
            PlaySceneBGM(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// sceneが変わった時にBGMを変える処理
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    private void OnChangeScene(Scene nextScene, LoadSceneMode mode)
    {
        PlaySceneBGM(nextScene.name);
    }

    /// <summary>
    /// ロードされたsceneNameと合うものを_audioDictionaryから探してPlayする
    /// </summary>
    /// <param name="sceneName"></param>
    void PlaySceneBGM(string sceneName)
    {
        if (_audioDectionary.GetTable().ContainsKey(sceneName))
        {
            var a = _audioDectionary.GetList().FirstOrDefault(dict => dict.Key == sceneName);
            _audioSource.clip = a.Value;
            _audioSource.Play();
        }
    }
}

/// <summary>
/// シーン名と流す音をもつAudioDataクラス
/// </summary>
[System.Serializable]
public class SceneAudioDictionary : SerializeDictonary<string, AudioClip, SceneAudio>
{
}


[System.Serializable]
public class SceneAudio : KeyAndValue<string, AudioClip>
{
    public SceneAudio (string sceneName, AudioClip sceneAudio) : base (sceneName, sceneAudio)
    {

    }
}