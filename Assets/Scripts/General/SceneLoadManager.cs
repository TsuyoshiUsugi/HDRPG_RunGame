using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SceneLoadManager : SingletonMonobehavior<SceneLoadManager>
{
    [SerializeField] Image _fadeImage;
    [SerializeField] float _fadeDuration;

    private void Start()
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void LoadScene(string sceneName)
    {
        if (!_fadeImage) SceneManager.LoadScene(sceneName);
        _fadeImage.DOFade(1, _fadeDuration).OnComplete(() => SceneManager.LoadScene(sceneName));
    }

    public async UniTask OnStartScene()
    {
        if (!_fadeImage) return;
        _fadeImage.color = new Color(0, 0, 0, 1);
        await _fadeImage.DOFade(0, _fadeDuration);
    }
}
