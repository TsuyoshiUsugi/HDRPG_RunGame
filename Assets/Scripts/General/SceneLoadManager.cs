using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoadManager : SingletonMonobehavior<SceneLoadManager>
{
    [SerializeField] Image _fadeImage;
    [SerializeField] float _fadeDuration;

    private void Awake()
    {
        _fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void LoadScene(string sceneName)
    {
        _fadeImage.DOFade(1, _fadeDuration).OnComplete(() => SceneManager.LoadScene(sceneName));
    }
}
