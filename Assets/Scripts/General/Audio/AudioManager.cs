using Serialize;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// Audioを操作するマネージャクラス
/// </summary>
public class AudioManager : SingletonMonobehavior<AudioManager>
{
    [Header("設定値")]
    [SerializeField] AudioDictionary _bgmDectionary;
    [SerializeField] AudioDictionary _seDictionary;
    [SerializeField] AudioSource _bgmAudioSource;
    [SerializeField] AudioSource _seAudioSource;
    [SerializeField] AudioMixer _mixer;
    string _bgmName = "BGM";
    string _seName = "SE";

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnChangeScene;

        if (_bgmAudioSource == null) return;
        if (_bgmAudioSource.isPlaying) return;
        
            PlayBGM(SceneManager.GetActiveScene().name);
        
    }

    /// <summary>
    /// sceneが変わった時にBGMを変える処理
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    private void OnChangeScene(Scene nextScene, LoadSceneMode mode)
    {
        PlayBGM(nextScene.name);
    }

    /// <summary>
    /// 引数の名前のBGMを鳴らす
    /// </summary>
    /// <param name="bgmName"></param>
    public void SetBGM(string bgmName)
    {
        PlayBGM(bgmName);
    }

    /// <summary>
    /// ロードされたsceneNameと合うものを_audioDictionaryから探してPlayする
    /// </summary>
    /// <param name="sceneName"></param>
    void PlayBGM(string sceneName)
    {
        if (!_bgmAudioSource) return;

        if (_bgmDectionary.GetTable().ContainsKey(sceneName))
        {
            var audioData = _bgmDectionary.GetList().FirstOrDefault(dict => dict.Key == sceneName);
            _bgmAudioSource.clip = audioData.Value;
            _bgmAudioSource.Play();
        }
    }

    /// <summary>
    /// 引数の名前のSEを鳴らす
    /// </summary>
    /// <param name="seName"></param>
    public void PlaySE(string seName)
    {
        var audioData = _seDictionary.GetList().FirstOrDefault(dict => dict.Key == seName);
        _seAudioSource.PlayOneShot(audioData.Value);
    }

    public void SetBGM(float vol)
    {
        _mixer.SetFloat(_bgmName, vol);
    }

    public void SetSE(float vol)
    {
        _mixer.SetFloat(_seName, vol);
    }
}

/// <summary>
/// シーン名と流す音をもつAudioDataクラス
/// </summary>
[System.Serializable]
public class AudioDictionary : SerializeDictonary<string, AudioClip, AudioKeyValueData>
{
}


[System.Serializable]
public class AudioKeyValueData : KeyAndValue<string, AudioClip>
{
    public AudioKeyValueData (string sceneName, AudioClip sceneAudio) : base (sceneName, sceneAudio)
    {

    }
}