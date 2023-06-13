using Serialize;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

/// <summary>
/// Audio�𑀍삷��}�l�[�W���N���X
/// </summary>
public class AudioManager : SingletonMonobehavior<AudioManager>
{
    [Header("�ݒ�l")]
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
    /// scene���ς��������BGM��ς��鏈��
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    private void OnChangeScene(Scene nextScene, LoadSceneMode mode)
    {
        PlaySceneBGM(nextScene.name);
    }

    /// <summary>
    /// ���[�h���ꂽsceneName�ƍ������̂�_audioDictionary����T����Play����
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
/// �V�[�����Ɨ�����������AudioData�N���X
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