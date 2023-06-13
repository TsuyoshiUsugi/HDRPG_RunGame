using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Audio�𑀍삷��}�l�[�W���N���X
/// </summary>
public class AudioManager : SingletonMonobehavior<AudioManager>
{
    [Header("�ݒ�l")]
    [SerializeField] Dictionary<string, AudioClip> _audioDectionary;

    AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _audioManager);
        SceneManager.sceneLoaded += OnChangeScene;
    }

    /// <summary>
    /// scene���ς��������BGM��ς��鏈��
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    private void OnChangeScene(Scene nextScene, LoadSceneMode mode)
    {
        
    }
}
